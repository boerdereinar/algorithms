using Algorithms.Sorting.Algorithms.ExchangeSorts;
using Algorithms.Sorting.Algorithms.HybridSorts;

namespace Algorithms.Sorting.Tests.Algorithms.HybridSorts;

/// <summary>
/// Tests for <see cref="GnomeSort{TKey}"/>.
/// </summary>
public sealed class MultiKeyQuickSortTests : SortingAlgorithmTestBase<MultiKeyQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(MultiKeyQuickSort);
}
