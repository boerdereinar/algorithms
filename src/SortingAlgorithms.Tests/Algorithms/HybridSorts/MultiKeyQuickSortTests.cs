using SortingAlgorithms.Algorithms.ExchangeSorts;
using SortingAlgorithms.Algorithms.HybridSorts;

namespace SortingAlgorithms.Tests.Algorithms.HybridSorts;

/// <summary>
/// Tests for <see cref="GnomeSort{TKey}"/>.
/// </summary>
public sealed class MultiKeyQuickSortTests : SortingAlgorithmTestBase<MultiKeyQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(MultiKeyQuickSort);
}
