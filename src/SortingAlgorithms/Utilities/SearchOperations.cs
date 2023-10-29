using System.Diagnostics.Contracts;

namespace SortingAlgorithms.Utilities;

/// <summary>
/// Search operations on lists.
/// </summary>
internal static class SearchOperations
{
	/// <summary>
	/// Searches the entire sorted <see cref="IReadOnlyList{T}"/> for an element using the specified comparer and
	/// returns the zero-based index of the element.
	/// </summary>
	/// <param name="source">The sorted sequence of values to search.</param>
	/// <param name="key">The key to locate.</param>
	/// <param name="keySelector">A function to extract a key from an element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare keys.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of key returned by <paramref name="keySelector"/>.</typeparam>
	/// <returns>
	/// The index of the specified value in the specified array, if value is found; otherwise, a negative number.
	/// If value is not found and value is less than one or more elements in array, the negative number returned is the
	/// bitwise complement of the index of the first element that is larger than value. If value is not found and value
	/// is greater than all elements in array, the negative number returned is the bitwise complement of (the index of
	/// the last element plus 1). If this method is called with a non-sorted array, the return value can be incorrect
	/// and a negative number could be returned, even if value is present in array.
	/// </returns>
	[Pure]
	public static int BinarySearch<TSource, TKey>(this IReadOnlyList<TSource> source, TKey key, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var l = 0;
		var r = source.Count - 1;
		while (l <= r)
		{
			var m = (l + r) / 2;
			switch (comparer.Compare(keySelector(source[m]), key))
			{
				case 0:
					return m;
				case < 0:
					l = m + 1;
					break;
				case > 0:
					r = m - 1;
					break;
			}
		}

		return ~l;
	}
}
