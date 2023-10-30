using Algorithms.Sorting.Algorithms.Concurrent;

namespace Algorithms.Sorting.Tests.Algorithms.Concurrent;

/// <summary>
/// Tests for <see cref="BatcherOddEvenMergeSort{TKey}"/>.
/// </summary>
public sealed class BatcherOddEvenMergeSortTests : SortingAlgorithmTestBase<BatcherOddEvenMergeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BatcherOddEvenMergeSort<>);
}
