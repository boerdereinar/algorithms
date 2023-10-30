using Algorithms.Sorting.Algorithms.Hybrid;

namespace Algorithms.Sorting.Tests.Algorithms.Hybrid;

/// <summary>
/// Tests for <see cref="TimSort{TKey}"/>.
/// </summary>
public sealed class TimSortTests : SortingAlgorithmTestBase<TimSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(TimSort<>);
}
