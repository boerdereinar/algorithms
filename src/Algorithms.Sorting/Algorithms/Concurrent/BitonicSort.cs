using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Concurrent;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Bitonic_sorter">Bitonic Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BitonicSort<TKey> : ISortingAlgorithm<TKey>
{
	private readonly SemaphoreSlim _semaphore;

	/// <summary>
	/// Initializes a new instance of the <see cref="BitonicSort{TKey}"/> class.
	/// </summary>
	public BitonicSort() : this(2 * Environment.ProcessorCount) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BitonicSort{TKey}"/> class.
	/// </summary>
	/// <param name="maxDegreeOfParallelism">The maximum number of concurrent tasks.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than one.</exception>
	public BitonicSort(int maxDegreeOfParallelism)
	{
		if (maxDegreeOfParallelism < 1)
			throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "The maximum degree of parallelism must be greater than zero.");

		_semaphore = new(maxDegreeOfParallelism, maxDegreeOfParallelism);
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BitonicSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		Sort<TSource>(keyedArray, false);
		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BitonicSort<T>();
	}

	private void Sort<TSource>(KeyedArraySegment<TSource, TKey> source, bool descending)
	{
		if (source.Length <= 1)
			return;

		var m = source.Length / 2;
		if (_semaphore.Wait(0))
		{
			Parallel.Invoke(
				() => Sort(source[..m], !descending),
				() => Sort(source[m..], descending));
			_semaphore.Release();
		}
		else
		{
			Sort(source[..m], !descending);
			Sort(source[m..], descending);
		}

		Merge(source, descending);
	}

	private void Merge<TSource>(KeyedArraySegment<TSource, TKey> source, bool descending)
	{
		if (source.Length <= 1)
			return;

		var m = 1 << int.Log2(source.Length - 1);
		for (var i = 0; i < source.Length - m; i++)
			if (source.Compare(i, i + m) > 0 ^ descending)
				source.Swap(i, i + m);

		if (_semaphore.Wait(0))
		{
			Parallel.Invoke(
				() => Merge(source[..m], descending),
				() => Merge(source[m..], descending));
			_semaphore.Release();
		}
		else
		{
			Merge(source[..m], descending);
			Merge(source[m..], descending);
		}
	}
}
