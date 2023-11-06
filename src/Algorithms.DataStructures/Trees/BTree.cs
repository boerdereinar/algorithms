using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Algorithms.DataStructures.Trees;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/B-tree">B-Tree</a> data structure.
/// </summary>
public sealed class BTree : ITraversableTree<int>
{
	[ExcludeFromCodeCoverage]
	private BTree() { }

	/// <inheritdoc />
	public static ITraversableTree<TValue, TKey> Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return BTree<TValue, TKey>.Create(source, keySelector, comparer);
	}

	/// <summary>
	/// Creates a <see cref="ITraversableTree{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="m">The order of the tree.</param>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
	/// <returns>The <see cref="ITraversableTree{TValue,TKey}"/>.</returns>
	public static ITraversableTree<TValue, TKey> Create<TValue, TKey>(int m, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return BTree<TValue, TKey>.Create(m, source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/B-tree">B-Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class BTree<TValue, TKey> : ITraversableTree<TValue, TKey, int>
{
	private readonly int _m;
	private readonly IComparer<TKey> _comparer;
	private readonly TValue[] _values;
	private readonly TKey[] _keys;
	private readonly BTree<TValue, TKey>[] _children;

	/// <summary>
	/// Initializes a new instance of the <see cref="BTree{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	public BTree(IComparer<TKey> comparer)
		: this(2, comparer) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BTree{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="m">The order of the tree.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <exception cref="ArgumentOutOfRangeException"><paramref name="m"/> is less than 1.</exception>
	public BTree(int m, IComparer<TKey> comparer)
		: this(m, comparer, true) { }

	private BTree(int m, IComparer<TKey> comparer, bool isLeaf)
	{
		if (m < 1)
			throw new ArgumentOutOfRangeException(nameof(m), m, "The order of the tree must be at least 1.");

		_m = m;
		_comparer = comparer;
		IsLeaf = isLeaf;

		_values = new TValue[2 * m - 1];
		_keys = new TKey[2 * m - 1];
		_children = new BTree<TValue, TKey>[isLeaf ? 0 : 2 * m];
	}

	/// <summary>
	/// Gets the values of the tree node.
	/// </summary>
	public ReadOnlySpan<TValue> Values => _values.AsSpan(..N);

	/// <summary>
	/// Gets the keys of the tree node.
	/// </summary>
	public ReadOnlySpan<TKey> Keys => _keys.AsSpan(..N);

	/// <summary>
	/// Gets the children of the tree node.
	/// </summary>
	public ReadOnlySpan<BTree<TValue, TKey>> Children => IsLeaf ? default : _children.AsSpan(..(N + 1));

	/// <summary>
	/// Gets the number of values of the tree node.
	/// </summary>
	public int N { get; private set; }

	/// <summary>
	/// Gets a value indicating whether the tree node is a leaf.
	/// </summary>
	public bool IsLeaf { get; }

	/// <summary>
	/// Inserts a value into the tree.
	/// </summary>
	/// <param name="value">The value to insert into the tree.</param>
	/// <param name="key">The key to insert into the tree.</param>
	/// <returns>The new root of the tree.</returns>
	public BTree<TValue, TKey> Insert(TValue value, TKey key)
	{
		if (N == 0)
		{
			_values[0] = value;
			_keys[0] = key;
			N++;
			return this;
		}

		if (N == _values.Length)
		{
			var root = new BTree<TValue, TKey>(_m, _comparer, false);
			root._children[0] = this;
			root.SplitChild(0);

			var i = _comparer.Compare(root._keys[0], key) < 0 ? 1 : 0;
			root._children[i]?.InsertNonFull(value, key);
			return root;
		}

		InsertNonFull(value, key);
		return this;
	}

	/// <inheritdoc />
	public IEnumerator<TValue> GetEnumerator()
	{
		for (var i = 0; i < N; i++)
		{
			if (!IsLeaf && _children[i] is { } child)
				foreach (var item in child)
					yield return item;

			yield return _values[i];
		}

		if (!IsLeaf && _children[N] is { } lastChild)
			foreach (var item in lastChild)
				yield return item;
	}

	/// <inheritdoc />
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <inheritdoc />
	public static ITraversableTree<TValue, TKey> Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		var root = new BTree<TValue, TKey>(comparer);
		foreach (var item in source)
			root = root.Insert(item, keySelector(item));

		return root;
	}

	/// <summary>
	/// Creates a <see cref="ITraversableTree{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="m">The order of the tree.</param>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The <see cref="ITraversableTree{TValue,TKey}"/>.</returns>
	public static ITraversableTree<TValue, TKey> Create(int m, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		var root = new BTree<TValue, TKey>(m, comparer);
		foreach (var item in source)
			root = root.Insert(item, keySelector(item));

		return root;
	}

	private void InsertNonFull(TValue value, TKey key)
	{
		var i = N - 1;

		if (IsLeaf)
		{
			for (; i >= 0 && _comparer.Compare(_keys[i], key) > 0; i--)
			{
				_keys[i + 1] = _keys[i];
				_values[i + 1] = _values[i];
			}

			_keys[i + 1] = key;
			_values[i + 1] = value;
			N++;
		}
		else
		{
			while (i >= 0 && _comparer.Compare(_keys[i], key) > 0)
				i--;

			if (_children[i + 1].N == 2 * _m - 1)
			{
				SplitChild(i + 1);

				if (_comparer.Compare(_keys[i + 1], key) < 0)
					i++;
			}

			_children[i + 1].InsertNonFull(value, key);
		}
	}

	private void SplitChild(int i)
	{
		var child = _children[i];
		var node = new BTree<TValue, TKey>(_m, _comparer, child.IsLeaf) { N = _m - 1 };

		for (var j = 0; j < _m - 1; j++)
		{
			node._keys[j] = child._keys[j + _m];
			node._values[j] = child._values[j + _m];
		}

		if (!child.IsLeaf)
			for (var j = 0; j < _m; j++)
				node._children[j] = child._children[j + _m];

		child.N = _m - 1;

		for (var j = N; j >= i + 1; j--)
			_children[j + 1] = _children[j];

		_children[i + 1] = node;

		for (var j = N - 1; j >= i; j--)
		{
			_keys[j + 1] = _keys[j];
			_values[j + 1] = _values[j];
		}

		_keys[i] = child._keys[_m - 1];
		_values[i] = child._values[_m - 1];
		N++;
	}
}
