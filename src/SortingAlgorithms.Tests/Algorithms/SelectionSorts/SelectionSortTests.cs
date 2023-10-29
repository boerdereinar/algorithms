using SortingAlgorithms.Algorithms.SelectionSorts;

namespace SortingAlgorithms.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="SelectionSort{TKey}"/>.
/// </summary>
public sealed class SelectionSortTests : SortingAlgorithmTestBase<SelectionSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(SelectionSort<>);
}
