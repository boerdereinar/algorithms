using Algorithms.Common.Collections;

namespace Algorithms.Common.Utilities;

/// <summary>
/// Extension methods for <see cref="Random"/>.
/// </summary>
public static class RandomExtensions
{
	/// <summary>
	/// Shuffles the elements of a <see cref="KeyedArray{TElement,TKey}"/>.
	/// </summary>
	/// <param name="random">The random number generator.</param>
	/// <param name="source">A <see cref="KeyedArray{TElement,TKey}"/> to shuffle.</param>
	/// <typeparam name="TElement">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in <paramref name="source"/>.</typeparam>
	public static void Shuffle<TElement, TKey>(this Random random, KeyedArray<TElement, TKey> source)
	{
		for (var i = 0; i < source.Length; i++)
		{
			var j = random.Next(i, source.Length);
			source.Swap(i, j);
		}
	}

	/// <summary>
	/// Shuffles the elements of a <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	/// <param name="random">The random number generator.</param>
	/// <param name="source">A <see cref="KeyedArraySegment{TElement,TKey}"/> to shuffle.</param>
	/// <typeparam name="TElement">The type of the elements of <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key used to compare elements in <paramref name="source"/>.</typeparam>
	public static void Shuffle<TElement, TKey>(this Random random, KeyedArraySegment<TElement, TKey> source)
	{
		for (var i = 0; i < source.Length; i++)
		{
			var j = random.Next(i, source.Length);
			source.Swap(i, j);
		}
	}
}
