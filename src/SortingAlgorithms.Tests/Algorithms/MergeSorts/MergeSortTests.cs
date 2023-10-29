using SortingAlgorithms.Algorithms.MergeSorts;

namespace SortingAlgorithms.Tests.Algorithms.MergeSorts;

/// <summary>
/// Tests for <see cref="MergeSort{TKey}"/>.
/// </summary>
public sealed class MergeSortTests : SortingAlgorithmTestBase<MergeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(MergeSort<>);
}
