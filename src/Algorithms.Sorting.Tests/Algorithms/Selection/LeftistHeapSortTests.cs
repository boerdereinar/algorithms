using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="LeftistHeapSort{TKey}"/>.
/// </summary>
public sealed class LeftistHeapSortTests : SortingAlgorithmTestBase<LeftistHeapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(LeftistHeapSort<>);
}
