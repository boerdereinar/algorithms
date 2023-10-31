using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Algorithms.DataStructures.Trees;

/// <summary>
/// Represent the <a href="https://en.wikipedia.org/wiki/Binary_search_tree">Binary Search Tree</a> data structure.
/// </summary>
public sealed class BinaryTree : ITraversableTree
{
	/// <inheritdoc />
	public static ITraversableTree<TSource, TKey>? Create<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return BinaryTree<TSource, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represent the <a href="https://en.wikipedia.org/wiki/Binary_search_tree">Binary Search Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class BinaryTree<TValue, TKey> : ITraversableTree<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;

	/// <summary>
	/// Initializes a new instance of the <see cref="BinaryTree{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="value">The value of the tree node.</param>
	/// <param name="key">The key of the tree node.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	public BinaryTree(TValue value, TKey key, IComparer<TKey> comparer)
	{
		Value = value;
		Key = key;
		_comparer = comparer;
	}

	/// <summary>
	/// Gets the value of the tree node.
	/// </summary>
	public TValue Value { get; private set; }

	/// <summary>
	/// Gets the key of the tree node.
	/// </summary>
	public TKey Key { get; private set; }

	/// <summary>
	/// Gets the left descendant of the tree.
	/// </summary>
	public BinaryTree<TValue, TKey>? Left { get; private set; }

	/// <summary>
	/// Gets the right descendant of the tree.
	/// </summary>
	public BinaryTree<TValue, TKey>? Right { get; private set; }

	/// <summary>
	/// Inserts a value into the tree.
	/// </summary>
	/// <param name="value">The value to insert into the tree.</param>
	/// <param name="key">The key to insert into the tree.</param>
	public void Insert(TValue value, TKey key)
	{
		var tree = this;
		while (true)
		{
			if (_comparer.Compare(key, tree.Key) < 0)
			{
				if (tree.Left is null)
				{
					tree.Left = new(value, key, _comparer);
					return;
				}

				tree = tree.Left;
			}
			else
			{
				if (tree.Right is null)
				{
					tree.Right = new(value, key, _comparer);
					return;
				}

				tree = tree.Right;
			}
		}
	}

	/// <inheritdoc />
	public IEnumerator<TValue> GetEnumerator()
	{
		if (Left is not null)
			foreach (var value in Left) yield return value;

		yield return Value;

		if (Right is not null)
			foreach (var value in Right) yield return value;
	}

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <inheritdoc />
	public static ITraversableTree<TValue, TKey>? Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		using var enumerator = source.GetEnumerator();
		if (!enumerator.MoveNext())
			return null;

		var tree = new BinaryTree<TValue, TKey>(enumerator.Current, keySelector(enumerator.Current), comparer);
		while (enumerator.MoveNext())
			tree.Insert(enumerator.Current, keySelector(enumerator.Current));

		return tree;
	}
}
