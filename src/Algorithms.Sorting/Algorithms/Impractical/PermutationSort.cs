using System.Diagnostics;
using Algorithms.Common.Collections;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Impractical;

/// <summary>
/// Represents the Permutation Sort sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class PermutationSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new PermutationSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		foreach (var permutation in Permutations<TSource>(keyedArray))
			if (permutation.IsSorted())
				return permutation;

		throw new UnreachableException();
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new PermutationSort<T>();
	}

	private static IEnumerable<KeyedArray<TSource, TKey>> Permutations<TSource>(KeyedArraySegment<TSource, TKey> source)
	{
		if (source.Length == 0)
		{
			yield return source.Array;
			yield break;
		}

		for (var i = 0; i < source.Length; i++)
		{
			source.Swap(0, i);

			foreach (var permutation in Permutations(source[1..]))
				yield return permutation;

			source.Swap(0, i);
		}
	}
}
