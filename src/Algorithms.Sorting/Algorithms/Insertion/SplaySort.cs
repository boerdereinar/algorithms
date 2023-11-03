using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Splaysort">Splaysort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of the elements to be sorted.</typeparam>
public sealed class SplaySort<TKey> : TreeSort<TKey, SplayTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new SplaySort<TKey>();
}
