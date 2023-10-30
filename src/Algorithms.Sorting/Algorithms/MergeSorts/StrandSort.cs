using System.Diagnostics;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.MergeSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Strand_sort">Strand Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class StrandSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new StrandSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var sourceList = new LinkedList<(TSource Item, TKey Key)>(source.Select(x => (x, keySelector(x))));
		var sorted = new LinkedList<(TSource Item, TKey Key)>();

		while (sourceList.First is { } first)
		{
			var next = first.Next;
			var subList = new LinkedList<(TSource Item, TKey Key)>();
			sourceList.Remove(first);
			subList.AddLast(first);
			Debug.Assert(subList.Last is not null, "subList.Last is not null");

			while (next is { } cur)
			{
				next = cur.Next;
				if (comparer.Compare(cur.Value.Key, subList.Last.Value.Key) <= 0)
					continue;

				sourceList.Remove(cur);
				subList.AddLast(cur);
			}

			Merge(sorted, subList, comparer);
		}

		return sorted.Select(x => x.Item);
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new StrandSort<T>();
	}

	private static void Merge<TSource>(LinkedList<(TSource Item, TKey Key)> a, LinkedList<(TSource Item, TKey Key)> b, IComparer<TKey> comparer)
	{
		var aCur = a.First;
		var next = b.First;
		while (next is { } cur)
		{
			if (aCur is not null && comparer.Compare(cur.Value.Key, aCur.Value.Key) > 0)
				aCur = aCur.Next;
			else
			{
				next = cur.Next;
				b.Remove(cur);
				if (aCur is null)
					a.AddLast(cur);
				else
					a.AddBefore(aCur, cur);
			}
		}
	}
}
