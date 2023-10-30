using Algorithms.Sorting.Algorithms.Merge;

namespace Algorithms.Sorting.Tests.Algorithms.Merge;

/// <summary>
/// Tests for <see cref="StrandSort{TKey}"/>.
/// </summary>
public sealed class StrandSortTests : SortingAlgorithmTestBase<StrandSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(StrandSort<>);
}
