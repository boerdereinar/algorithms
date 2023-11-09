using System.Collections.Concurrent;
using System.Numerics;
using Algorithms.Common.Collections;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Impractical;

/// <summary>
/// Represents the <a href="https://archive.fo/xhGo">Sleep Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class SleepSort<TKey> : ISortingAlgorithm<TKey> where TKey : INumber<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new SleepSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.MinMax() is not var (minKey, maxKey))
			return keyedArray;

		var min = double.CreateChecked(minKey);
		var max = double.CreateChecked(maxKey);

		var queue = new ConcurrentQueue<TSource>();
		Parallel.ForEachAsync(Enumerable.Range(0, keyedArray.Length).Select(i => keyedArray[i]), async (x, token) =>
		{
			var s = double.CreateChecked(x.Key) - min;
			if (min > max) s *= -1;

			await Task.Delay(TimeSpan.FromSeconds(s), token).ConfigureAwait(false);
			queue.Enqueue(x.Element);
		}).Wait();

		return queue;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		throw new NotSupportedException();
	}
}
