using System.Numerics;
using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Pigeonhole_sort">Pigeonhole Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class PigeonholeSort<TKey> : ISortingAlgorithm<TKey> where TKey : IBinaryInteger<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new PigeonholeSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.MinMax() is not var (minKey, maxKey))
			return Enumerable.Empty<TSource>();

		var min = int.CreateChecked(minKey);
		var max = int.CreateChecked(maxKey);
		var n = Math.Abs(max - min) + 1;
		var pigeonHoles = new List<TSource>?[n];

		for (var i = 0; i < keyedArray.Length; i++)
		{
			var (element, key) = keyedArray[i];
			var hole = Math.Abs(int.CreateChecked(key) - min);

			pigeonHoles[hole] ??= new();
			pigeonHoles[hole]?.Add(element);
		}

		return pigeonHoles.OfType<List<TSource>>().SelectMany(x => x);
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}
