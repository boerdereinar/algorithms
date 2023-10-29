using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace SortingAlgorithms.Sorting;

/// <inheritdoc cref="IOrderedEnumerable{TElement}"/>
internal class OrderedEnumerable<TSource, TCompositeKey> : IOrderedEnumerable<TSource>
{
	private readonly IEnumerable<TSource> _source;
	private readonly Func<TSource, TCompositeKey> _keySelector;
	private readonly IComparer<TCompositeKey> _comparer;
	private readonly ISortingAlgorithm<TCompositeKey> _sortingAlgorithm;

	/// <summary>
	/// Initializes a new instance of the <see cref="OrderedEnumerable{TSource, TCompositeKey}"/> class.
	/// </summary>
	/// <param name="source">A sequence of values to order.</param>
	/// <param name="keySelector">The <see cref="Func{T, TResult}"/> used to extract the key for each element.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> used to compare keys for placement in the returned sequence.</param>
	/// <param name="sortingAlgorithm">The algorithm used to sort the elements.</param>
	public OrderedEnumerable(
		IEnumerable<TSource> source,
		Func<TSource, TCompositeKey> keySelector,
		IComparer<TCompositeKey> comparer,
		ISortingAlgorithm<TCompositeKey> sortingAlgorithm)
	{
		_source = source;
		_keySelector = keySelector;
		_comparer = comparer;
		_sortingAlgorithm = sortingAlgorithm;
	}

	/// <inheritdoc/>
	public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey>? comparer, bool descending)
	{
		comparer ??= Comparer<TKey>.Default;
		if (descending)
			comparer = comparer.Reverse();

		return new OrderedEnumerable<TSource, CompositeKey<TCompositeKey, TKey>>(
			_source,
			x => new(_keySelector(x), keySelector(x)),
			new CompositeKey<TCompositeKey, TKey>.Comparer(_comparer, comparer),
			_sortingAlgorithm.CreateComposite<CompositeKey<TCompositeKey, TKey>>());
	}

	/// <inheritdoc/>
	public IEnumerator<TSource> GetEnumerator()
	{
		return _sortingAlgorithm.OrderBy(_source, _keySelector, _comparer).GetEnumerator();
	}

	/// <inheritdoc/>
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
