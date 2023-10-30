using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="SelectionSort{TKey}"/>.
/// </summary>
public sealed class SelectionSortTests : SortingAlgorithmTestBase<SelectionSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(SelectionSort<>);
}
