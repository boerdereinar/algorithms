using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="DefaultHeapSort{TKey}"/>.
/// </summary>
public sealed class DefaultHeapSortTests : SortingAlgorithmTestBase<DefaultHeapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(DefaultHeapSort<>);
}
