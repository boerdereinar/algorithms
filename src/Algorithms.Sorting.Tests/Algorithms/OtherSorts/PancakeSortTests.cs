using Algorithms.Sorting.Algorithms.OtherSorts;

namespace Algorithms.Sorting.Tests.Algorithms.OtherSorts;

/// <summary>
/// Tests for <see cref="PancakeSort{TKey}"/>.
/// </summary>
public sealed class PancakeSortTests : SortingAlgorithmTestBase<PancakeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PancakeSort<>);
}
