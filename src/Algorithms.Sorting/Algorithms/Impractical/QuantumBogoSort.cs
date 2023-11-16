using Algorithms.Common.Collections;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Impractical;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Bogosort#Related_algorithms">Quantum Bogosort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class QuantumBogoSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new QuantumBogoSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (!keyedArray.IsSorted())
			throw new EndOfTheUniverseException();

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new QuantumBogoSort<T>();
	}
}

/// <summary>
/// Exception thrown when the universe ends.
/// </summary>
public sealed class EndOfTheUniverseException : Exception;
