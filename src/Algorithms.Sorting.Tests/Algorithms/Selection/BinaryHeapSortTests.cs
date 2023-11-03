using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="BinaryHeapSort{TKey}"/>.
/// </summary>
public sealed class BinaryHeapSortTests : SortingAlgorithmTestBase<BinaryHeapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BinaryHeapSort<>);
}
