using SortingAlgorithms.Algorithms.MergeSorts;

namespace SortingAlgorithms.Tests.Algorithms.MergeSorts;

/// <summary>
/// Tests for <see cref="StrandSort{TKey}"/>.
/// </summary>
public sealed class StrandSortTests : SortingAlgorithmTestBase<StrandSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(StrandSort<>);
}
