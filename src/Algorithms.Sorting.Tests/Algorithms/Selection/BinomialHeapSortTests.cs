using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="BinomialHeapSort{TKey}"/>.
/// </summary>
public sealed class BinomialHeapSortTests : SortingAlgorithmTestBase<BinomialHeapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BinomialHeapSort<>);
}
