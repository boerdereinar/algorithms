using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="DualPivotQuickSort{TKey}"/>.
/// </summary>
public sealed class DualPivotQuickSortTests : SortingAlgorithmTestBase<DualPivotQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(DualPivotQuickSort<>);
}
