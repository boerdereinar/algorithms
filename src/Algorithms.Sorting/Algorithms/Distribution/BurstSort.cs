using Algorithms.Sorting.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Burstsort">Burstsort</a> sorting algorithm.
/// </summary>
public sealed class BurstSort : ISortingAlgorithm<string>
{
	private readonly int _bucketInitialSize;
	private readonly int _bucketGrowthFactor;
	private readonly int _bucketThreshold;

	/// <summary>
	/// Initializes a new instance of the <see cref="BurstSort"/> class.
	/// </summary>
	public BurstSort() : this(4, 4, 32) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BurstSort"/> class.
	/// </summary>
	/// <param name="initialSize">Initial size of the buckets.</param>
	/// <param name="growthFactor">Growth factor of the buckets.</param>
	/// <param name="threshold">Threshold of the buckets.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// <paramref name="initialSize"/> is less than one
	/// - or -
	/// <paramref name="growthFactor"/> is less than two
	/// - or -
	/// <paramref name="threshold"/> is less than <paramref name="initialSize"/>.
	/// </exception>
	public BurstSort(int initialSize, int growthFactor, int threshold)
	{
		if (initialSize < 1)
			throw new ArgumentOutOfRangeException(nameof(initialSize), initialSize, "Initial size must be greater than zero.");
		if (growthFactor < 2)
			throw new ArgumentOutOfRangeException(nameof(growthFactor), growthFactor, "Growth factor must be greater than one.");
		if (threshold < initialSize)
			throw new ArgumentOutOfRangeException(nameof(threshold), threshold, "Threshold must be greater than or equal to the initial size.");

		_bucketInitialSize = initialSize;
		_bucketGrowthFactor = growthFactor;
		_bucketThreshold = threshold;
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<string> Default { get; } = new BurstSort();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, string> keySelector, IComparer<string> comparer)
	{
		var trie = new BurstTrie<TSource>(_bucketInitialSize, _bucketGrowthFactor, _bucketThreshold);
		trie.InsertRange(source, keySelector);
		trie.Sort(CharComparer.FromStringComparer(comparer));

		return trie;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}
