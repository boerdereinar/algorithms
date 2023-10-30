using Algorithms.Sorting.Algorithms.Merge;

namespace Algorithms.Sorting.Tests.Algorithms.Merge;

/// <summary>
/// Tests for <see cref="MergeSort{TKey}"/>.
/// </summary>
public sealed class MergeSortTests : SortingAlgorithmTestBase<MergeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(MergeSort<>);
}
