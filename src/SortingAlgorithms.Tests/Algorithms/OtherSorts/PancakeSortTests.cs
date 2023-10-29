using SortingAlgorithms.Algorithms.OtherSorts;

namespace SortingAlgorithms.Tests.Algorithms.OtherSorts;

/// <summary>
/// Tests for <see cref="PancakeSort{TKey}"/>.
/// </summary>
public sealed class PancakeSortTests : SortingAlgorithmTestBase<PancakeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PancakeSort<>);
}
