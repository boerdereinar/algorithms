using Algorithms.Sorting.Algorithms.HybridSorts;

namespace Algorithms.Sorting.Tests.Algorithms.HybridSorts;

/// <summary>
/// Tests for <see cref="TimSort{TKey}"/>.
/// </summary>
public sealed class TimSortTests : SortingAlgorithmTestBase<TimSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(TimSort<>);
}
