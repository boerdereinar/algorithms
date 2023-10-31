using System.Diagnostics.CodeAnalysis;
using Algorithms.DataStructures.Heaps;

namespace Algorithms.DataStructures.Trees;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class LeftistTree<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;

	/// <summary>
	/// Initializes a new instance of the <see cref="LeftistTree{TValue,TKey}"/> class.
	/// </summary>
	/// <param name="value">The value of the tree node.</param>
	/// <param name="key">The key of the tree node.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	public LeftistTree(TValue value, TKey key, IComparer<TKey> comparer)
	{
		_comparer = comparer;
		Value = value;
		Key = key;
	}

	/// <summary>
	/// Gets the value of the tree node.
	/// </summary>
	public TValue Value { get; }

	/// <summary>
	/// Gets the key of the tree node.
	/// </summary>
	public TKey Key { get; }

	/// <summary>
	/// Gets the rank of the tree.
	/// </summary>
	public int Rank { get; private set; } = 1;

	/// <summary>
	/// Gets the left descendant of the tree.
	/// </summary>
	public LeftistTree<TValue, TKey>? Left { get; private set; }

	/// <summary>
	/// Gets the right descendant of the tree.
	/// </summary>
	public LeftistTree<TValue, TKey>? Right { get; private set; }

	/// <summary>
	/// Creates a <see cref="LeftistHeap{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The <see cref="LeftistTree{TValue,TKey}"/>.</returns>
	public static LeftistTree<TValue, TKey>? Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		var trees = new Queue<LeftistTree<TValue, TKey>>(source.Select(x => new LeftistTree<TValue, TKey>(x, keySelector(x), comparer)));
		if (trees.Count == 0)
			return null;

		while (trees.Count > 1)
			trees.Enqueue(Merge(trees.Dequeue(), trees.Dequeue()));

		return trees.Dequeue();
	}

	/// <summary>
	/// Merges two leftist trees.
	/// </summary>
	/// <param name="left">The left tree.</param>
	/// <param name="right">The right tree.</param>
	/// <returns>The merged tree.</returns>
	[return: NotNullIfNotNull(nameof(left))]
	[return: NotNullIfNotNull(nameof(right))]
	public static LeftistTree<TValue, TKey>? Merge(LeftistTree<TValue, TKey>? left, LeftistTree<TValue, TKey>? right)
	{
		if (right is null)
			return left;

		if (left is null)
			return right;

		if (left._comparer.Compare(left.Key, right.Key) > 0)
			(left, right) = (right, left);

		left.Right = Merge(left.Right, right);
		if (left.Left is null)
			(left.Left, left.Right) = (left.Right, left.Left);
		else
		{
			if (left.Left.Rank < left.Right.Rank)
				(left.Left, left.Right) = (left.Right, left.Left);

			left.Rank = left.Right.Rank + 1;
		}

		return left;
	}
}
