namespace Algorithms.DataStructures.Trees;

/// <summary>
/// Represents a tree data structure that can be traversed and returns a sorted collection.
/// </summary>
public interface ITraversableTree
{
	/// <summary>
	/// Creates a <see cref="ITraversableTree{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
	/// <returns>The <see cref="ITraversableTree{TValue,TKey}"/>.</returns>
	static abstract ITraversableTree<TValue, TKey>? Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer);
}

/// <summary>
/// Represents a tree data structure that can be traversed and returns a sorted collection.
/// </summary>
/// <typeparam name="TArgument">The type of argument of the traversable tree.</typeparam>
public interface ITraversableTree<in TArgument> : ITraversableTree
{
	/// <summary>
	/// Creates a <see cref="ITraversableTree{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="argument">The argument of the traversable tree.</param>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
	/// <returns>The <see cref="ITraversableTree{TValue,TKey}"/>.</returns>
	static abstract ITraversableTree<TValue, TKey>? Create<TValue, TKey>(TArgument argument, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer);
}

/// <summary>
/// Represents a tree data structure that can be traversed and returns a sorted collection.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
public interface ITraversableTree<TValue, TKey> : IEnumerable<TValue>
{
	/// <summary>
	/// Creates a <see cref="ITraversableTree{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The <see cref="ITraversableTree{TValue,TKey}"/>.</returns>
	static abstract ITraversableTree<TValue, TKey>? Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer);
}

/// <summary>
/// Represents a tree data structure that can be traversed and returns a sorted collection.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the tree.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the tree.</typeparam>
/// <typeparam name="TArgument">The type of the argument of the traversable tree.</typeparam>
public interface ITraversableTree<TValue, TKey, in TArgument> : ITraversableTree<TValue, TKey>
{
	/// <summary>
	/// Creates a <see cref="ITraversableTree{TValue,TKey}"/> from a collection.
	/// </summary>
	/// <param name="argument">The argument of the traversable tree.</param>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The <see cref="ITraversableTree{TValue,TKey}"/>.</returns>
	static abstract ITraversableTree<TValue, TKey>? Create(TArgument argument, IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer);
}
