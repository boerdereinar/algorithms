using Algorithms.Sorting.Algorithms.ConcurrentSorts;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.ConcurrentSorts;

/// <summary>
/// Tests for <see cref="ParallelMergeSort{TKey}"/>.
/// </summary>
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
