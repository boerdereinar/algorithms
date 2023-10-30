using Algorithms.Sorting.Algorithms.MergeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.MergeSorts;

/// <summary>
/// Tests for <see cref="MergeSort{TKey}"/>.
/// </summary>
public sealed class MergeSortTests : SortingAlgorithmTestBase<MergeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(MergeSort<>);
}
