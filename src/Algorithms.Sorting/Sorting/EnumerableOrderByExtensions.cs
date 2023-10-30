using System.Diagnostics.Contracts;

namespace Algorithms.Sorting.Sorting;

/// <summary>
/// OrderBy extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
public static class EnumerableOrderByExtensions
{
	/// <summary>
	/// Sorts the elements of a sequence in ascending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> Order<TSource, TAlgorithm>(this IEnumerable<TSource> source)
		where TAlgorithm : ISortingAlgorithm<TSource>
	{
		return source.OrderBy<TSource, TSource, TAlgorithm>(x => x);
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> Order<TSource, TAlgorithm>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		where TAlgorithm : ISortingAlgorithm<TSource>
	{
		return source.OrderBy<TSource, TSource, TAlgorithm>(x => x, comparer);
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted according to a key.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey, TAlgorithm>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		where TAlgorithm : ISortingAlgorithm<TKey>
	{
		return source.OrderBy<TSource, TKey, TAlgorithm>(keySelector, Comparer<TKey>.Default);
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted according to a key.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey, TAlgorithm>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		where TAlgorithm : ISortingAlgorithm<TKey>
	{
		return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, TAlgorithm.Default);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderDescending<TSource, TAlgorithm>(this IEnumerable<TSource> source)
		where TAlgorithm : ISortingAlgorithm<TSource>
	{
		return source.OrderByDescending<TSource, TSource, TAlgorithm>(x => x);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderDescending<TSource, TAlgorithm>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		where TAlgorithm : ISortingAlgorithm<TSource>
	{
		return source.OrderByDescending<TSource, TSource, TAlgorithm>(x => x, comparer);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order according to a key.
	/// </returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey, TAlgorithm>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		where TAlgorithm : ISortingAlgorithm<TKey>
	{
		return source.OrderByDescending<TSource, TKey, TAlgorithm>(keySelector, Comparer<TKey>.Default);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <typeparam name="TAlgorithm">The type of the algorithm used to sort the elements.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order according to a key.
	/// </returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey, TAlgorithm>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		where TAlgorithm : ISortingAlgorithm<TKey>
	{
		return source.OrderBy<TSource, TKey, TAlgorithm>(keySelector, comparer.Reverse());
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> Order<TSource>(this IEnumerable<TSource> source, ISortingAlgorithm<TSource> algorithm)
	{
		return source.OrderBy<TSource, TSource>(x => x, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> Order<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer, ISortingAlgorithm<TSource> algorithm)
	{
		return source.OrderBy<TSource, TSource>(x => x, comparer, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted according to a key.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, ISortingAlgorithm<TKey> algorithm)
	{
		return source.OrderBy(keySelector, Comparer<TKey>.Default, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in ascending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted according to a key.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, ISortingAlgorithm<TKey> algorithm)
	{
		return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderDescending<TSource>(this IEnumerable<TSource> source, ISortingAlgorithm<TSource> algorithm)
	{
		return source.OrderByDescending<TSource, TSource>(x => x, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <returns>An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order.</returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderDescending<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer, ISortingAlgorithm<TSource> algorithm)
	{
		return source.OrderByDescending<TSource, TSource>(x => x, comparer, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order according to a key.
	/// </returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, ISortingAlgorithm<TKey> algorithm)
	{
		return source.OrderByDescending(keySelector, Comparer<TKey>.Default, algorithm);
	}

	/// <summary>
	/// Sorts the elements of a sequence in descending order according to a key.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <param name="algorithm">The algorithm used to sort the elements.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> whose elements are sorted in descending order according to a key.
	/// </returns>
	[Pure]
	public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer, ISortingAlgorithm<TKey> algorithm)
	{
		return source.OrderBy(keySelector, comparer.Reverse(), algorithm);
	}
}
