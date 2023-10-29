using SortingAlgorithms.Algorithms.ConcurrentSorts;

namespace SortingAlgorithms.Tests.Algorithms.ConcurrentSorts;

/// <summary>
/// Tests for <see cref="BatcherOddEvenMergeSort{TKey}"/>.
/// </summary>
public sealed class BatcherOddEvenMergeSortTests : SortingAlgorithmTestBase<BatcherOddEvenMergeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BatcherOddEvenMergeSort<>);
}
