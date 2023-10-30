using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Concurrent;

/// <summary>
/// Represents a parallel implementation of the <a href="https://en.wikipedia.org/wiki/Quicksort">Quicksort</a> sorting
/// algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class ParallelQuickSort<TKey> : ISortingAlgorithm<TKey>
{
	private readonly SemaphoreSlim _semaphore;

	/// <summary>
	/// Initializes a new instance of the <see cref="ParallelQuickSort{TKey}"/> class.
	/// </summary>
	public ParallelQuickSort() : this(2 * Environment.ProcessorCount) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="ParallelQuickSort{TKey}"/> class.
	/// </summary>
	/// <param name="maxDegreeOfParallelism">The maximum number of concurrent tasks.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than one.</exception>
	public ParallelQuickSort(int maxDegreeOfParallelism)
	{
		if (maxDegreeOfParallelism < 1)
			throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "The maximum degree of parallelism must be greater than zero.");

		_semaphore = new(maxDegreeOfParallelism, maxDegreeOfParallelism);
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new ParallelQuickSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		Sort<TSource>(keyedArray);
		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new ParallelQuickSort<T>();
	}

	private void Sort<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Length <= 1)
			return;

		var pivot = Partition(source);
		if (_semaphore.Wait(0))
		{
			Parallel.Invoke(
				() => Sort(source[..pivot]),
				() => Sort(source[(pivot + 1)..]));
			_semaphore.Release();
		}
		else
		{
			Sort(source[..pivot]);
			Sort(source[(pivot + 1)..]);
		}
	}

	private static int Partition<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		var pivot = source.Key(^1);
		var i = 0;
		for (var j = 0; j < source.Length - 1; j++)
			if (source.Compare(j, pivot) < 0)
				source.Swap(i++, j);

		source.Swap(i, ^1);
		return i;
	}
}
