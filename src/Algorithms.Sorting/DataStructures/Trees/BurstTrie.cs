using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Algorithms.Sorting.Algorithms.DistributionSorts;
using Algorithms.Sorting.Algorithms.HybridSorts;
using Algorithms.Sorting.Algorithms.InsertionSorts;
using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.DataStructures.Trees;

/// <summary>
/// Represents the Burst <a href="https://en.wikipedia.org/wiki/Trie">Trie</a> for <see cref="BurstSort"/>.
/// </summary>
/// <typeparam name="T">The type of elements in the trie.</typeparam>
public sealed class BurstTrie<T> : IEnumerable<T>
{
	private readonly int _bucketInitialSize;
	private readonly int _bucketGrowthFactor;
	private readonly int _bucketThreshold;
	private readonly int _depth;

	private readonly Dictionary<int, IBucket> _buckets = new();
	private readonly List<char?> _keys;

	/// <summary>
	/// Initializes a new instance of the <see cref="BurstTrie{TSource}"/> class.
	/// </summary>
	public BurstTrie() : this(4, 4, 32) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BurstTrie{TSource}"/> class.
	/// </summary>
	/// <param name="initialSize">Initial size of the buckets.</param>
	/// <param name="growthFactor">Growth factor of the buckets.</param>
	/// <param name="threshold">Threshold of the buckets.</param>
	public BurstTrie(int initialSize, int growthFactor, int threshold) : this(initialSize, growthFactor, threshold, 0) { }

	private BurstTrie(int initialSize, int growthFactor, int threshold, int depth)
	{
		_bucketInitialSize = initialSize;
		_bucketGrowthFactor = growthFactor;
		_bucketThreshold = threshold;
		_depth = depth;
		_keys = new();
	}

	/// <summary>
	/// Inserts a value into the burst trie.
	/// </summary>
	/// <param name="value">The value to insert.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	public void Insert(T value, Func<T, string> keySelector)
	{
		Insert(value, keySelector(value));
	}

	/// <summary>
	/// Inserts a value into the burst trie.
	/// </summary>
	/// <param name="value">The value to insert.</param>
	/// <param name="key">The key of the value to insert.</param>
	public void Insert(T value, string key)
	{
		var c = key.TryGetCharAt(_depth);
		if (!_buckets.TryGetValue(c ?? -1, out var bucket))
		{
			_keys.Add(c);
			if (c is null)
				bucket = _buckets[-1] = new DuplicateBucket();
			else
				bucket = _buckets[c.Value] = new Bucket(_bucketInitialSize, _bucketGrowthFactor, _bucketThreshold, _depth);
		}

		bucket.Insert(value, key);
	}

	/// <summary>
	/// Inserts a range of values into the burst trie.
	/// </summary>
	/// <param name="values">The values to insert.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	public void InsertRange(IEnumerable<T> values, Func<T, string> keySelector)
	{
		foreach (var value in values)
			Insert(value, keySelector);
	}

	/// <summary>
	/// Sorts the burst trie.
	/// </summary>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	public void Sort(IComparer<char?> comparer)
	{
		InsertionSort<char?>.Sort(_keys, 0, _keys.Count, comparer);

		foreach (var (_, bucket) in _buckets)
			bucket.Sort(comparer);
	}

	/// <inheritdoc />
	public IEnumerator<T> GetEnumerator()
	{
		return _keys.SelectMany(key => _buckets[key ?? -1]).GetEnumerator();
	}

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>
	/// Burst trie bucket.
	/// </summary>
	private interface IBucket : IEnumerable<T>
	{
		/// <summary>
		/// Inserts a value into the bucket.
		/// </summary>
		/// <param name="value">The value to insert.</param>
		/// <param name="key">The key of the value to insert.</param>
		void Insert(T value, string key);

		/// <summary>
		/// Sorts the values in the bucket.
		/// </summary>
		/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
		void Sort(IComparer<char?> comparer);
	}

	/// <summary>
	/// Burst trie bucket.
	/// </summary>
	private sealed class Bucket : IBucket
	{
		private readonly int _bucketInitialSize;
		private readonly int _bucketGrowthFactor;
		private readonly int _bucketThreshold;
		private readonly int _depth;

		private KeyedArray<T, string> _bucket;
		private int _pointer;
		private BurstTrie<T>? _trie;

		/// <summary>
		/// Initializes a new instance of the <see cref="Bucket"/> class.
		/// </summary>
		/// <param name="initialSize">Initial size of the buckets.</param>
		/// <param name="growthFactor">Growth factor of the buckets.</param>
		/// <param name="threshold">Threshold of the buckets.</param>
		/// <param name="depth">The current depth of the bucket.</param>
		public Bucket(int initialSize, int growthFactor, int threshold, int depth)
		{
			_bucketInitialSize = initialSize;
			_bucketGrowthFactor = growthFactor;
			_bucketThreshold = threshold;
			_depth = depth;

			_bucket = new(_bucketInitialSize, Comparer<string>.Default);
		}

		/// <inheritdoc />
		public void Insert(T value, string key)
		{
			if (_trie is not null)
				_trie.Insert(value, key);
			else if (_pointer == _bucketThreshold)
			{
				_trie = new(_bucketInitialSize, _bucketGrowthFactor, _bucketThreshold, _depth + 1);
				for (var i = 0; i < _bucketThreshold; i++)
					_trie.Insert(_bucket.Element(i), _bucket.Key(i));
				_trie.Insert(value, key);
				_bucket = null!;
			}
			else
			{
				if (_pointer == _bucket.Length)
					_bucket.Resize(Math.Min(_bucket.Length * _bucketGrowthFactor, _bucketThreshold));

				_bucket[_pointer++] = (value, key);
			}
		}

		/// <inheritdoc />
		public void Sort(IComparer<char?> comparer)
		{
			if (_trie is not null)
				_trie.Sort(comparer);
			else
				MultiKeyQuickSort.Sort(_bucket[.._pointer], _depth, comparer);
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			return (_trie as IEnumerable<T> ?? _bucket[.._pointer]).GetEnumerator();
		}

		/// <inheritdoc />
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	/// <summary>
	/// Bucket to store duplicate items.
	/// </summary>
	private sealed class DuplicateBucket : IBucket
	{
		private readonly List<T> _bucket = new();

		/// <inheritdoc />
		public void Insert(T value, string key)
		{
			_bucket.Add(value);
		}

		/// <inheritdoc />
		public void Sort(IComparer<char?> comparer) { }

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			return _bucket.GetEnumerator();
		}

		/// <inheritdoc />
		[ExcludeFromCodeCoverage]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
