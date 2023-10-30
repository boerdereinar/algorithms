using System.Numerics;
using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.Algorithms.DistributionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Proxmap_sort">Proxmap Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class ProxmapSort<TKey> : ISortingAlgorithm<TKey> where TKey : struct, INumber<TKey>
{
	private readonly int _buckets;

	/// <summary>
	/// Initializes a new instance of the <see cref="ProxmapSort{TKey}"/> class.
	/// </summary>
	public ProxmapSort() : this(10) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="ProxmapSort{TKey}"/> class.
	/// </summary>
	/// <param name="buckets">Number of buckets.</param>
	public ProxmapSort(int buckets)
	{
		if (buckets < 1)
			throw new ArgumentOutOfRangeException(nameof(buckets), buckets, "Number of buckets must greater than zero.");

		_buckets = buckets;
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new ProxmapSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.MinMax() is not var (minKey, maxKey))
			return Enumerable.Empty<TSource>();

		var min = double.CreateChecked(minKey);
		var max = double.CreateChecked(maxKey);
		var range = Math.Abs(max - min) + 1;

		var keys = new int[keyedArray.Length];
		var hits = new int[_buckets];
		for (var i = 0; i < keyedArray.Length; i++)
		{
			var key = keyedArray.Key(i);
			var bucket = (int)(_buckets * Math.Abs(double.CreateChecked(key) - min) / range);

			// Handle infinity
			bucket = Math.Clamp(bucket, 0, _buckets - 1);

			keys[i] = bucket;
			hits[bucket]++;
		}

		hits[^1] = keyedArray.Length - hits[^1];
		for (var i = _buckets - 1; i > 0; i--)
			hits[i - 1] = hits[i] - hits[i - 1];

		var output = new KeyedArray<TSource, TKey?>(keyedArray.Length, null!);
		for (var i = 0; i < keyedArray.Length; i++)
		{
			var index = hits[keys[i]];
			var start = index;
			while (output.Key(index) is not null)
				index++;

			for (; index > start && output.Key(index - 1) is { } key && keyedArray.Compare(i, key) < 0; index--)
				output[index] = output[index - 1];

			output[index] = keyedArray[i];
		}

		return output;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}
