namespace Algorithms.Sorting.DataStructures.Trees;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Binomial_heap">Binomial Tree</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public sealed class BinomialTree<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;
	private readonly List<BinomialTree<TValue, TKey>> _forest = new();

	/// <summary>
	/// Initializes a new instance of the <see cref="BinomialTree{TSource,TKey}"/> class.
	/// </summary>
	/// <param name="value">The value of the tree node.</param>
	/// <param name="key">The key of the tree node.</param>
	public BinomialTree(TValue value, TKey key) : this(value, key, Comparer<TKey>.Default) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="BinomialTree{TSource,TKey}"/> class.
	/// </summary>
	/// <param name="value">The value of the tree node.</param>
	/// <param name="key">The key of the tree node.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public BinomialTree(TValue value, TKey key, IComparer<TKey> comparer)
	{
		Value = value;
		Key = key;
		_comparer = comparer;
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
	/// Gets the order of the tree.
	/// </summary>
	public int Order => Forest.Count;

	/// <summary>
	/// Gets the sub forest of the tree.
	/// </summary>
	public IReadOnlyList<BinomialTree<TValue, TKey>> Forest => _forest.AsReadOnly();

	/// <summary>
	/// Merges two binomial trees.
	/// </summary>
	/// <param name="left">The left tree.</param>
	/// <param name="right">The right tree.</param>
	/// <returns>The merged tree.</returns>
	/// <exception cref="ArgumentException">Trees must have the same order.</exception>
	public static BinomialTree<TValue, TKey> Merge(BinomialTree<TValue, TKey> left, BinomialTree<TValue, TKey> right)
	{
		if (left.Order != right.Order)
			throw new ArgumentException("Trees must have the same order.", nameof(right));

		if (left._comparer.Compare(left.Key, right.Key) > 0)
			(left, right) = (right, left);

		left._forest.Add(right);
		return left;
	}
}
