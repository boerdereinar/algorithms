using SortingAlgorithms.Algorithms.SelectionSorts;

namespace SortingAlgorithms.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="CycleSort{TKey}"/>.
/// </summary>
public sealed class CycleSortTests : SortingAlgorithmTestBase<CycleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CycleSort<>);
}
