using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.ImpracticalSorts;

/// <summary>
/// Represents the <a href="https://arxiv.org/abs/2110.01111">I Can't Believe It Can Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class ICannotBelieveItCanSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new ICannotBelieveItCanSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		for (var i = 0; i < keyedArray.Length; i++)
			for (var j = 0; j < keyedArray.Length; j++)
				if (keyedArray.Compare(i, j) < 0)
					keyedArray.Swap(i, j);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new ICannotBelieveItCanSort<T>();
	}
}
