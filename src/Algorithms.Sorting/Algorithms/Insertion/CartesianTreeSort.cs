using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Insertion;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Cartesian_tree#In_sorting">Cartesian Tree Sort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class CartesianTreeSort<TKey> : TreeSort<TKey, CartesianTree>
{
	/// <inheritdoc cref="TreeSort{TKey,TTree}.Default"/>
	public static new ISortingAlgorithm<TKey> Default { get; } = new CartesianTreeSort<TKey>();
}
