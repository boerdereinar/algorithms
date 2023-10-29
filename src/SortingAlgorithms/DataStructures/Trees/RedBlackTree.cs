using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace SortingAlgorithms.DataStructures.Trees;

/// <summary>
/// The color of a node in the <a href="https://en.wikipedia.org/wiki/Red%E2%80%93black_tree">Red-Black Tree</a>.
/// </summary>
public enum Color
{
	/// <summary>
	/// Red.
	/// </summary>
	Red,

	/// <summary>
	/// Black.
	/// </summary>
	Black,
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Red%E2%80%93black_tree">Red-Black Tree</a> data structure.
/// </summary>
public sealed class RedBlackTree : ITraversableTree
{
	/// <inheritdoc />
	public static ITraversableTree<TValue, TKey>? Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return RedBlackTree<TValue, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Red%E2%80%93black_tree">Red-Black Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class RedBlackTree<TValue, TKey> : ITraversableTree<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;

	/// <summary>
	/// Initializes a new instance of the <see cref="RedBlackTree{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="value">The value of the tree node.</param>
	/// <param name="key">The key of the tree node.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	public RedBlackTree(TValue value, TKey key, IComparer<TKey> comparer)
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
	/// Gets the color of the tree node.
	/// </summary>
	public Color Color { get; private set; } = Color.Black;

	/// <summary>
	/// Gets the left descendant of the tree.
	/// </summary>
	public RedBlackTree<TValue, TKey>? Left { get; private set; }

	/// <summary>
	/// Gets the right descendant of the tree.
	/// </summary>
	public RedBlackTree<TValue, TKey>? Right { get; private set; }

	/// <summary>
	/// Gets the parent of the tree.
	/// </summary>
	public RedBlackTree<TValue, TKey>? Parent { get; private set; }

	/// <summary>
	/// Inserts a value into the tree.
	/// </summary>
	/// <param name="value">The value to insert into the tree.</param>
	/// <param name="key">The key to insert into the tree.</param>
	/// <returns>The new root of the tree.</returns>
	public RedBlackTree<TValue, TKey> Insert(TValue value, TKey key)
	{
		var tree = this;
		var node = new RedBlackTree<TValue, TKey>(value, key, _comparer) { Color = Color.Red };
		while (true)
		{
			if (_comparer.Compare(key, tree.Key) < 0)
			{
				if (tree.Left is null)
				{
					tree.Left = node;
					break;
				}

				tree = tree.Left;
			}
			else
			{
				if (tree.Right is null)
				{
					tree.Right = node;
					break;
				}

				tree = tree.Right;
			}
		}

		node.Parent = tree;
		return InsertFixup(this, node);
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

	/// <inheritdoc />
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

		var root = new RedBlackTree<TValue, TKey>(enumerator.Current, keySelector(enumerator.Current), comparer);
		while (enumerator.MoveNext())
			root = root.Insert(enumerator.Current, keySelector(enumerator.Current));

		return root;
	}

	private static RedBlackTree<TValue, TKey> RotateLeft(RedBlackTree<TValue, TKey> root, RedBlackTree<TValue, TKey> node)
	{
		Debug.Assert(node.Right is not null, "node.Right is not null");

		var right = node.Right;
		node.Right = right.Left;
		if (right.Left is not null)
			right.Left.Parent = node;

		right.Parent = node.Parent;
		if (node.Parent is null)
			root = right;
		else if (ReferenceEquals(node.Parent.Left, node))
			node.Parent.Left = right;
		else
			node.Parent.Right = right;

		right.Left = node;
		node.Parent = right;

		return root;
	}

	private static RedBlackTree<TValue, TKey> RotateRight(RedBlackTree<TValue, TKey> root, RedBlackTree<TValue, TKey> node)
	{
		Debug.Assert(node.Left is not null, "node.Right is not null");

		var left = node.Left;
		node.Left = left.Right;
		if (left.Right is not null)
			left.Right.Parent = node;

		left.Parent = node.Parent;
		if (node.Parent is null)
			root = left;
		else if (ReferenceEquals(node.Parent.Left, node))
			node.Parent.Left = left;
		else
			node.Parent.Right = left;

		left.Right = node;
		node.Parent = left;

		return root;
	}

	private static RedBlackTree<TValue, TKey> InsertFixup(RedBlackTree<TValue, TKey> root, RedBlackTree<TValue, TKey> node)
	{
		while (node.Parent is { Color: Color.Red, Parent: not null })
		{
			if (ReferenceEquals(node.Parent, node.Parent.Parent.Left))
			{
				var uncle = node.Parent.Parent.Right;
				if (uncle?.Color is Color.Red)
				{
					node.Parent.Color = Color.Black;
					uncle.Color = Color.Black;
					node.Parent.Parent.Color = Color.Red;
					node = node.Parent.Parent;
				}
				else
				{
					if (ReferenceEquals(node, node.Parent.Right))
					{
						node = node.Parent;
						root = RotateLeft(root, node);
					}

					Debug.Assert(node.Parent is not null, "node.Parent is not null");
					Debug.Assert(node.Parent.Parent is not null, "node.Parent.Parent is not null");

					node.Parent.Color = Color.Black;
					node.Parent.Parent.Color = Color.Red;
					root = RotateRight(root, node.Parent.Parent);
				}
			}
			else
			{
				var uncle = node.Parent.Parent.Left;
				if (uncle?.Color is Color.Red)
				{
					node.Parent.Color = Color.Black;
					uncle.Color = Color.Black;
					node.Parent.Parent.Color = Color.Red;
					node = node.Parent.Parent;
				}
				else
				{
					if (ReferenceEquals(node, node.Parent.Left))
					{
						node = node.Parent;
						root = RotateRight(root, node);
					}

					Debug.Assert(node.Parent is not null, "node.Parent is not null");
					Debug.Assert(node.Parent.Parent is not null, "node.Parent.Parent is not null");

					node.Parent.Color = Color.Black;
					node.Parent.Parent.Color = Color.Red;
					root = RotateLeft(root, node.Parent.Parent);
				}
			}
		}

		root.Color = Color.Black;
		return root;
	}
}
