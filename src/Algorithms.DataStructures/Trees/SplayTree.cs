using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Algorithms.DataStructures.Trees;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Splay_tree">Splay Tree</a> data structure.
/// </summary>
public sealed class SplayTree : ITraversableTree
{
	/// <inheritdoc />
	public static ITraversableTree<TValue, TKey>? Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return SplayTree<TValue, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Splay_tree">Splay Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class SplayTree<TValue, TKey> : ITraversableTree<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;

	/// <summary>
	/// Initializes a new instance of the <see cref="SplayTree{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="value">The value of the tree node.</param>
	/// <param name="key">The key of the tree node.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public SplayTree(TValue value, TKey key, IComparer<TKey> comparer)
	{
		Values = new(1) { value };
		Key = key;
		_comparer = comparer;
	}

	/// <summary>
	/// Gets the values of the tree node.
	/// </summary>
	public List<TValue> Values { get; private set; }

	/// <summary>
	/// Gets the key of the tree node.
	/// </summary>
	public TKey Key { get; private set; }

	/// <summary>
	/// Gets the left descendant of the tree.
	/// </summary>
	public SplayTree<TValue, TKey>? Left { get; private set; }

	/// <summary>
	/// Gets the right descendant of the tree.
	/// </summary>
	public SplayTree<TValue, TKey>? Right { get; private set; }

	/// <summary>
	/// Inserts a value into the tree.
	/// </summary>
	/// <param name="value">The value to insert into the tree.</param>
	/// <param name="key">The key to insert into the tree.</param>
	/// <returns>The new root of the tree.</returns>
	public SplayTree<TValue, TKey> Insert(TValue value, TKey key)
	{
		var root = Splay(key);
		var compare = _comparer.Compare(root.Key, key);

		if (compare == 0)
		{
			root.Values.Add(value);
			return root;
		}

		var node = new SplayTree<TValue, TKey>(value, key, _comparer);
		if (compare > 0)
		{
			node.Left = root.Left;
			node.Right = root;
			root.Left = null;
		}
		else
		{
			node.Left = root;
			node.Right = root.Right;
			root.Right = null;
		}

		return node;
	}

	/// <inheritdoc />
	public IEnumerator<TValue> GetEnumerator()
	{
		if (Left is not null)
			foreach (var value in Left) yield return value;

		foreach (var value in Values) yield return value;

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

		var root = new SplayTree<TValue, TKey>(enumerator.Current, keySelector(enumerator.Current), comparer);
		while (enumerator.MoveNext())
			root = root.Insert(enumerator.Current, keySelector(enumerator.Current));

		return root;
	}

	private SplayTree<TValue, TKey> RotateRight()
	{
		if (Left is null)
			return this;

		var x = Left;
		Left = x.Right;
		x.Right = this;
		return x;
	}

	private SplayTree<TValue, TKey> RotateLeft()
	{
		if (Right is null)
			return this;

		var x = Right;
		Right = x.Left;
		x.Left = this;
		return x;
	}

	private SplayTree<TValue, TKey> Splay(TKey key)
	{
		switch (_comparer.Compare(Key, key))
		{
			case 0:
				return this;
			case > 0:
				if (Left is null)
					return this;

				switch (_comparer.Compare(Left.Key, key))
				{
					case > 0:
						Left.Left = Left.Left?.Splay(key);
						return RotateRight().RotateRight();
					case < 0:
						Left.Right = Left.Right?.Splay(key);
						Left = Left.RotateLeft();
						break;
				}

				return RotateRight();
			case < 0:
				if (Right is null)
					return this;

				switch (_comparer.Compare(Right.Key, key))
				{
					case > 0:
						Right.Left = Right.Left?.Splay(key);
						Right = Right.RotateRight();
						break;
					case < 0:
						Right.Right = Right.Right?.Splay(key);
						return RotateLeft().RotateLeft();
				}

				return RotateLeft();
		}
	}
}
