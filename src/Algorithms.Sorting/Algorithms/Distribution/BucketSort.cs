using System.Numerics;
using Algorithms.Common.Collections;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Algorithms.Insertion;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Bucket_sort">Bucket Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BucketSort<TKey> : ISortingAlgorithm<TKey> where TKey : INumber<TKey>
{
	private readonly int _buckets;

	/// <summary>
	/// Initializes a new instance of the <see cref="BucketSort{TKey}"/> class.
	/// </summary>
	public BucketSort() : this(10) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BucketSort{TKey}"/> class.
	/// </summary>
	/// <param name="buckets">Number of buckets.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="buckets"/> is less than one.</exception>
	public BucketSort(int buckets)
	{
		if (buckets < 1)
			throw new ArgumentOutOfRangeException(nameof(buckets), buckets, "Number of buckets must be greater than zero.");

		_buckets = buckets;
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BucketSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.MinMax() is not var (minKey, maxKey))
			return Enumerable.Empty<TSource>();

		var min = double.CreateChecked(minKey);
		var max = double.CreateChecked(maxKey);
		var range = Math.Abs(max - min) + 1;

		var size = keyedArray.Length / _buckets + 1;
		var buckets = Enumerable.Range(0, _buckets).Select(_ => new KeyedArray<TSource, TKey>(size, comparer)).ToArray();
		var bucketSize = new int[_buckets];

		for (var i = 0; i < keyedArray.Length; i++)
		{
			var item = keyedArray[i];
			var bucket = (int)(_buckets * Math.Abs(double.CreateChecked(item.Key) - min) / range);

			// Handle infinity
			bucket = Math.Clamp(bucket, 0, _buckets - 1);

			if (bucketSize[bucket] == buckets[bucket].Length)
				buckets[bucket].Resize(bucketSize[bucket] * 2);

			buckets[bucket][bucketSize[bucket]++] = item;
		}

		for (var i = 0; i < _buckets; i++)
			InsertionSort<TKey>.Sort(buckets[i][..bucketSize[i]]);

		return buckets.SelectMany((x, i) => x[..bucketSize[i]]);
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}
