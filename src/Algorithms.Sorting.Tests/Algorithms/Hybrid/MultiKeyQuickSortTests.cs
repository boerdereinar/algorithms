using Algorithms.Sorting.Algorithms.Exchange;
using Algorithms.Sorting.Algorithms.Hybrid;

namespace Algorithms.Sorting.Tests.Algorithms.Hybrid;

/// <summary>
/// Tests for <see cref="GnomeSort{TKey}"/>.
/// </summary>
public sealed class MultiKeyQuickSortTests : SortingAlgorithmTestBase<MultiKeyQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(MultiKeyQuickSort);
}
