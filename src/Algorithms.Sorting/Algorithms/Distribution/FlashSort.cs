using System.Numerics;
using Algorithms.Common.Collections;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Algorithms.Insertion;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Flashsort">Flash Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class FlashSort<TKey> : ISortingAlgorithm<TKey> where TKey : INumber<TKey>
{
	private readonly int _buckets;

	/// <summary>
	/// Initializes a new instance of the <see cref="FlashSort{TKey}"/> class.
	/// </summary>
	public FlashSort() : this(10) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="FlashSort{TKey}"/> class.
	/// </summary>
	/// <param name="buckets">Number of buckets.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="buckets"/> is less than one.</exception>
	public FlashSort(int buckets)
	{
		if (buckets < 1)
			throw new ArgumentOutOfRangeException(nameof(buckets), buckets, "Number of buckets must be greater than zero.");

		_buckets = buckets;
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new FlashSort<TKey>();

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
		var bucketSize = new int[_buckets + 1];
		for (var i = 0; i < keyedArray.Length; i++)
		{
			var key = keyedArray.Key(i);
			var bucket = (int)(_buckets * Math.Abs(double.CreateChecked(key) - min) / range);

			// Handle infinity
			bucket = Math.Clamp(bucket, 0, _buckets - 1);

			keys[i] = bucket;
			bucketSize[bucket]++;
		}

		for (var i = 1; i <= _buckets; i++)
			bucketSize[i] += bucketSize[i - 1];

		var output = new KeyedArray<TSource, TKey>(keyedArray.Length, comparer);
		for (var i = 0; i < keyedArray.Length; i++)
		{
			var item = keyedArray[i];
			output[--bucketSize[keys[i]]] = item;
		}

		// BUG: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3708
#pragma warning disable SA1011
		for (var i = 0; i < _buckets; i++)
			InsertionSort<TKey>.Sort(output[bucketSize[i]..bucketSize[i + 1]]);
#pragma warning restore SA1011

		return output;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}
