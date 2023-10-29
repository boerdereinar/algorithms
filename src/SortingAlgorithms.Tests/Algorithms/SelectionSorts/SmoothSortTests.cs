using SortingAlgorithms.Algorithms.SelectionSorts;

namespace SortingAlgorithms.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="SmoothSort{TKey}"/>.
/// </summary>
public sealed class SmoothSortTests : SortingAlgorithmTestBase<SmoothSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(SmoothSort<>);
}
