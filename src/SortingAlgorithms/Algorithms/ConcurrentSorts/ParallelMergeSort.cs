using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Algorithms.ConcurrentSorts;

/// <summary>
/// Represents a parallel implementation of the <a href="https://en.wikipedia.org/wiki/Merge_sort#Merge_sort_with_parallel_merging">Merge Sort</a>
/// sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class ParallelMergeSort<TKey> : ISortingAlgorithm<TKey>
{
	private readonly SemaphoreSlim _semaphore;

	/// <summary>
	/// Initializes a new instance of the <see cref="ParallelMergeSort{TKey}"/> class.
	/// </summary>
	public ParallelMergeSort() : this(2 * Environment.ProcessorCount) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="ParallelMergeSort{TKey}"/> class.
	/// </summary>
	/// <param name="maxDegreeOfParallelism">The maximum number of concurrent tasks.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="maxDegreeOfParallelism"/> is less than one.</exception>
	public ParallelMergeSort(int maxDegreeOfParallelism)
	{
		if (maxDegreeOfParallelism < 1)
			throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "The maximum degree of parallelism must be greater than zero.");

		_semaphore = new(maxDegreeOfParallelism, maxDegreeOfParallelism);
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new ParallelMergeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var keyedClone = keyedArray.Clone();

		ParallelSplitMerge<TSource>(keyedArray, keyedClone);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new ParallelMergeSort<T>();
	}

	private void ParallelSplitMerge<TSource>(KeyedArraySegment<TSource, TKey> a, KeyedArraySegment<TSource, TKey> b)
	{
		if (a.Length <= 1)
			return;

		var middle = a.Length / 2;
		if (_semaphore.Wait(0))
		{
			Parallel.Invoke(
				() => ParallelSplitMerge(b[..middle], a[..middle]),
				() => ParallelSplitMerge(b[middle..], a[middle..]));
			_semaphore.Release();
		}
		else
		{
			ParallelSplitMerge(b[..middle], a[..middle]);
			ParallelSplitMerge(b[middle..], a[middle..]);
		}

		ParallelMerge(b[..middle], b[middle..], a);
	}

	private void ParallelMerge<TSource>(KeyedArraySegment<TSource, TKey> a, KeyedArraySegment<TSource, TKey> b, KeyedArraySegment<TSource, TKey> c)
	{
		if (a.Length < b.Length)
			(a, b) = (b, a);

		if (a.Length == 0)
			return;

		var r = a.Length / 2;
		var s = b.Keys.BinarySearch(a.Key(r), a.Array.Comparer);
		if (s < 0)
			s = ~s;

		var t = r + s;
		c[t] = a[r];

		if (_semaphore.Wait(0))
		{
			Parallel.Invoke(
				() => ParallelMerge(a[..r], b[..s], c[..t]),
				() => ParallelMerge(a[(r + 1)..], b[s..], c[(t + 1)..]));
			_semaphore.Release();
		}
		else
		{
			ParallelMerge(a[..r], b[..s], c[..t]);
			ParallelMerge(a[(r + 1)..], b[s..], c[(t + 1)..]);
		}
	}
}
