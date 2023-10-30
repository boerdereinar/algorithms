using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="DualPivotQuickSort{TKey}"/>.
/// </summary>
public sealed class DualPivotQuickSortTests : SortingAlgorithmTestBase<DualPivotQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(DualPivotQuickSort<>);
}
