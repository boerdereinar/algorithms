using Algorithms.Sorting.Algorithms.Other;

namespace Algorithms.Sorting.Tests.Algorithms.Other;

/// <summary>
/// Tests for <see cref="PancakeSort{TKey}"/>.
/// </summary>
public sealed class PancakeSortTests : SortingAlgorithmTestBase<PancakeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PancakeSort<>);
}
