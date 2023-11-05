using Algorithms.Common.Tests.TestUtilities;
using Algorithms.Sorting.Algorithms.Concurrent;

namespace Algorithms.Sorting.Tests.Algorithms.Concurrent;

/// <summary>
/// Tests for <see cref="ParallelMergeSort{TKey}"/>.
/// </summary>
[Collection(nameof(ConcurrentSortTestCollection))]
public sealed class ParallelMergeSortTests : SortingAlgorithmTestBase<ParallelMergeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(ParallelMergeSort<>);

	/// <summary>
	/// Tests if the constructor of <see cref="ParallelMergeSort{TKey}"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidMaxDegreeOfParallelism_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("maxDegreeOfParallelism", () => new ParallelMergeSort<int>(0));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
