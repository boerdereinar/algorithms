using System.Diagnostics.Contracts;
using Algorithms.Common.Collections;

namespace Algorithms.Common.Utilities;

/// <summary>
/// Extension methods for <see cref="KeyedArray{TElement,TKey}"/>.
/// </summary>
public static class KeyedArrayExtensions
{
	/// <summary>
	/// Checks if the elements of a <see cref="KeyedArray{TElement,TKey}"/> are sorted.
	/// </summary>
	/// <param name="source">A <see cref="KeyedArray{TElement,TKey}"/> to check.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
	/// <returns><c>true</c> if the elements are sorted, <c>false</c> otherwise.</returns>
	public static bool IsSorted<TSource, TKey>(this KeyedArray<TSource, TKey> source)
	{
		if (source.Length <= 1)
			return true;

		for (var i = 1; i < source.Length; i++)
			if (source.Compare(i - 1, i) > 0)
				return false;

		return true;
	}

	/// <summary>
	/// Returns the maximum and minimum key in a <see cref="KeyedArray{TElement,TKey}"/>.
	/// </summary>
	/// <param name="source">A <see cref="KeyedArray{TElement,TKey}"/> to determine the maximum and minimum key of.</param>
	/// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of key to compare elements by.</typeparam>
	/// <returns>The maximum and minimum key in <paramref name="source"/>.</returns>
	[Pure]
	public static (TKey Min, TKey Max)? MinMax<TSource, TKey>(this KeyedArray<TSource, TKey> source)
	{
		if (source.Length == 0)
			return null;

		var min = 0;
		var max = 0;
		for (var i = 1; i < source.Length; i++)
		{
			if (source.Compare(i, min) < 0)
				min = i;

			if (source.Compare(i, max) > 0)
				max = i;
		}

		return (source.Key(min), source.Key(max));
	}
}
