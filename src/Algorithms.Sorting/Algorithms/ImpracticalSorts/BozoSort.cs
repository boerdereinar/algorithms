using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.Algorithms.ImpracticalSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Bogosort#Related_algorithms">Bozosort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of the elements to be sorted.</typeparam>
public sealed class BozoSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BozoSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		var random = new Random(42);

		while (!keyedArray.IsSorted())
		{
			var (left, right) = RandomPair(random, keyedArray.Length);
			keyedArray.Swap(left, right);
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BozoSort<T>();
	}

	private static (int Left, int Right) RandomPair(Random random, int max)
	{
		var left = random.Next(max);
		for (var right = random.Next(max); ; right = random.Next(max))
			if (left != right)
				return (left, right);
	}
}
