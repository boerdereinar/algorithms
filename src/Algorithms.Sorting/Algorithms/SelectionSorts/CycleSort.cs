using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.SelectionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Cycle_sort">Cycle Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CycleSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new CycleSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		for (var cycleStart = 0; cycleStart < keyedArray.Length - 1; cycleStart++)
		{
			var item = keyedArray[cycleStart];

			var pos = cycleStart;
			for (var i = cycleStart + 1; i < keyedArray.Length; i++)
				if (keyedArray.Compare(i, item.Key) < 0)
					pos++;

			if (pos == cycleStart)
				continue;

			while (keyedArray.Compare(pos, item.Key) == 0)
				pos++;

			(item, keyedArray[pos]) = (keyedArray[pos], item);

			while (pos != cycleStart)
			{
				pos = cycleStart;
				for (var i = cycleStart + 1; i < keyedArray.Length; i++)
					if (keyedArray.Compare(i, item.Key) < 0)
						pos++;

				while (keyedArray.Compare(pos, item.Key) == 0)
					pos++;

				(item, keyedArray[pos]) = (keyedArray[pos], item);
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new CycleSort<T>();
	}
}
