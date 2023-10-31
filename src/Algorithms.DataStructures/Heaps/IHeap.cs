using System.Diagnostics.CodeAnalysis;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents a min-heap data structure.
/// </summary>
public interface IHeap
{
	/// <summary>
	/// Creates a min-heap from a collection.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
	/// <returns>The <see cref="IHeap{TSource,TKey}"/>.</returns>
	static abstract IHeap<TValue, TKey> Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer);
}

/// <summary>
/// Represents a min-heap data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public interface IHeap<TValue, TKey>
{
	/// <summary>
	/// Gets the number of elements in the heap.
	/// </summary>
	int Count { get; }

	/// <summary>
	/// Gets the minimum value in the heap.
	/// </summary>
	/// <exception cref="InvalidOperationException">The heap is empty.</exception>
	TValue Min { get; }

	/// <summary>
	/// Inserts a value into the heap.
	/// </summary>
	/// <param name="value">The value to insert.</param>
	/// <param name="key">The key to insert.</param>
	void Insert(TValue value, TKey key);

	/// <summary>
	/// Tries to get the minimum value from the heap.
	/// </summary>
	/// <param name="value">The retrieved value.</param>
	/// <param name="key">The retrieved key.</param>
	/// <returns><see langword="true"/> if the value was retrieved; otherwise, <see langword="false"/>.</returns>
	bool TryGetMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key);

	/// <summary>
	/// Deletes the minimum value from the heap.
	/// </summary>
	/// <returns>The minimum value.</returns>
	/// <exception cref="InvalidOperationException">The heap is empty.</exception>
	TValue DeleteMin();

	/// <summary>
	/// Tries to delete the minimum value from the heap.
	/// </summary>
	/// <param name="value">The deleted value.</param>
	/// <param name="key">The deleted key.</param>
	/// <returns><see langword="true"/> if the value was deleted; otherwise, <see langword="false"/>.</returns>
	bool TryDeleteMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key);

	/// <summary>
	/// Creates a min-heap from an array of elements.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <returns>The min-heap.</returns>
	static abstract IHeap<TValue, TKey> Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer);
}
