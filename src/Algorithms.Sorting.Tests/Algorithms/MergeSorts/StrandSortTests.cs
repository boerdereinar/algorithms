using Algorithms.Sorting.Algorithms.MergeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.MergeSorts;

/// <summary>
/// Tests for <see cref="StrandSort{TKey}"/>.
/// </summary>
public sealed class StrandSortTests : SortingAlgorithmTestBase<StrandSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(StrandSort<>);
}
