using System.Collections;
using System.Diagnostics.CodeAnalysis;
using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.DataStructures.Trees;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Cartesian_tree">Cartesian Tree</a> data structure.
/// </summary>
public sealed class CartesianTree : ITraversableTree
{
	/// <inheritdoc />
	public static ITraversableTree<TValue, TKey>? Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return CartesianTree<TValue, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Cartesian_tree">Cartesian Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class CartesianTree<TValue, TKey> : ITraversableTree<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;

	private CartesianTree(TValue value, TKey key, IComparer<TKey> comparer)
	{
		Value = value;
		Key = key;
		_comparer = comparer;
	}

	/// <summary>
	/// Gets the value of the tree node.
	/// </summary>
	public TValue Value { get; private init; }

	/// <summary>
	/// Gets the key of the tree node.
	/// </summary>
	public TKey Key { get; private init; }

	/// <summary>
	/// Gets the left descendant of the tree.
	/// </summary>
	public CartesianTree<TValue, TKey>? Left { get; private init; }

	/// <summary>
	/// Gets the right descendant of the tree.
	/// </summary>
	public CartesianTree<TValue, TKey>? Right { get; private init; }

	/// <inheritdoc />
	public IEnumerator<TValue> GetEnumerator()
	{
		var queue = new PriorityQueue<CartesianTree<TValue, TKey>, TKey>(_comparer);
		queue.Enqueue(this, Key);

		while (queue.Count > 0)
		{
			var tree = queue.Dequeue();

			yield return tree.Value;

			if (tree.Left is not null)
				queue.Enqueue(tree.Left, tree.Left.Key);
			if (tree.Right is not null)
				queue.Enqueue(tree.Right, tree.Right.Key);
		}
	}

	/// <inheritdoc />
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>
	/// Creates a <see cref="CartesianTree{TSource,TKey}"/> from a collection.
	/// </summary>
	/// <param name="source">The values to construct the tree with.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The created <see cref="CartesianTree{TSource,TKey}"/>.</returns>
	public static CartesianTree<TValue, TKey>? Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.Length == 0)
			return null;

		var parent = new int[keyedArray.Length];
		var left = new int[keyedArray.Length];
		var right = new int[keyedArray.Length];
		Array.Fill(parent, -1);
		Array.Fill(left, -1);
		Array.Fill(right, -1);

		var root = 0;
		for (var i = 1; i < keyedArray.Length; i++)
		{
			var last = i - 1;
			right[i] = -1;

			while (last != root && keyedArray.Compare(last, i) >= 0)
				last = parent[last];

			if (keyedArray.Compare(last, i) >= 0)
			{
				parent[root] = i;
				left[i] = root;
				root = i;
			}
			else if (right[last] == -1)
			{
				right[last] = i;
				parent[i] = last;
				left[i] = -1;
			}
			else
			{
				parent[right[last]] = i;
				left[i] = right[last];
				right[last] = i;
				parent[i] = last;
			}
		}

		return Create(keyedArray, root, left, right);
	}

	/// <inheritdoc />
	static ITraversableTree<TValue, TKey>? ITraversableTree<TValue, TKey>.Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return Create(source, keySelector, comparer);
	}

	private static CartesianTree<TValue, TKey>? Create(KeyedArray<TValue, TKey> source, int root, ReadOnlySpan<int> left, ReadOnlySpan<int> right)
	{
		if (root == -1)
			return null;

		var (value, key) = source[root];
		return new(value, key, source.Comparer)
		{
			Left = Create(source, left[root], left, right),
			Right = Create(source, right[root], left, right),
		};
	}
}
