using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="DualPivotQuickSort{TKey}"/>.
/// </summary>
public sealed class DualPivotQuickSortTests : SortingAlgorithmTestBase<DualPivotQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(DualPivotQuickSort<>);
}
