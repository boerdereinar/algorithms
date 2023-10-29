using SortingAlgorithms.Sorting;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Algorithms.InsertionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Patience_sorting">Patience Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class PatienceSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new PatienceSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var deck = new List<Stack<(TSource Element, TKey Key)>>();
		foreach (var item in source)
		{
			var key = keySelector(item);

			var index = deck.BinarySearch(key, x => x.Peek().Key, comparer);
			if (index < 0)
				index = ~index;

			if (index < deck.Count)
				deck[index].Push((item, key));
			else
				deck.Add(new(new[] { (item, key) }));
		}

		while (deck.Count > 0)
		{
			yield return deck[0].Pop().Element;

			if (deck[0].Count == 0)
			{
				deck[0] = deck[^1];
				deck.RemoveAt(deck.Count - 1);
			}

			Balance(deck, comparer);
		}
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new PatienceSort<T>();
	}

	private static void Balance<TSource>(IList<Stack<(TSource Element, TKey Key)>> deck, IComparer<TKey> comparer)
	{
		for (var root = 0; 2 * root + 1 < deck.Count;)
		{
			var left = 2 * root + 1;
			var swap = root;

			if (comparer.Compare(deck[swap].Peek().Key, deck[left].Peek().Key) > 0)
				swap = left;

			if (left + 1 < deck.Count && comparer.Compare(deck[swap].Peek().Key, deck[left + 1].Peek().Key) > 0)
				swap = left + 1;

			if (swap == root)
				return;

			(deck[root], deck[swap]) = (deck[swap], deck[root]);
			root = swap;
		}
	}
}
