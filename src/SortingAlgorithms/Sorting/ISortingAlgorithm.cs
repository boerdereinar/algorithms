namespace SortingAlgorithms.Sorting;

/// <summary>
/// Represents a sorting algorithm that can sort a collection of items.
/// </summary>
/// <typeparam name="TKey">The type of items to be sorted.</typeparam>
public interface ISortingAlgorithm<TKey>
{
	/// <summary>
	/// Gets the default instance of the sorting algorithm.
	/// </summary>
	static abstract ISortingAlgorithm<TKey> Default { get; }

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted.</returns>
	IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer);

	/// <summary>
	/// Creates an instance of <see cref="ISortingAlgorithm{TKey}"/> with type <c>T</c>.
	/// </summary>
	/// <typeparam name="T">Type of the key.</typeparam>
	/// <returns>Created instance.</returns>
	/// <exception cref="NotSupportedException">
	/// Type <c>T</c> is not supported.
	/// </exception>
	ISortingAlgorithm<T> CreateComposite<T>();
}
