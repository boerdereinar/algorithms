using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.Algorithms.ImpracticalSorts;

/// <summary>
/// Represents the <a href="https://www.dangermouse.net/esoteric/bogobogosort.html">Bogobogosort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class BogoBogoSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new BogoBogoSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var random = new Random(42);
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.Length == 0)
			return keyedArray;

		Sort<TSource>(keyedArray, random);

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new BogoBogoSort<T>();
	}

	private static TKey Sort<TSource>(KeyedArraySegment<TSource, TKey> source, Random random)
	{
		if (source.Length == 1)
			return source.Key(0);

		while (true)
		{
			var copy = source.Clone();
			while (copy.Compare(^1, Sort(copy[..^1], random)) < 0)
				random.Shuffle(copy);

			if (source.Keys.SequenceEqual(copy.Keys))
				return source.Key(^1);

			random.Shuffle(source);
		}
	}
}
