using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="WeakHeapSort{TKey}"/>.
/// </summary>
public sealed class WeakHeapSortTests : SortingAlgorithmTestBase<WeakHeapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(WeakHeapSort<>);
}
