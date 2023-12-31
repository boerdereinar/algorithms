using Algorithms.Common.Tests.TestUtilities;
using Algorithms.Sorting.Algorithms.Concurrent;

namespace Algorithms.Sorting.Tests.Algorithms.Concurrent;

/// <summary>
/// Tests for <see cref="ParallelQuickSort{TKey}"/>.
/// </summary>
[Collection(nameof(ConcurrentSortTestCollection))]
public sealed class ParallelQuickSortTests : SortingAlgorithmTestBase<ParallelQuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(ParallelQuickSort<>);

	/// <summary>
	/// Tests if the constructor of <see cref="ParallelQuickSort{TKey}"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidMaxDegreeOfParallelism_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("maxDegreeOfParallelism", () => new ParallelQuickSort<int>(0));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
