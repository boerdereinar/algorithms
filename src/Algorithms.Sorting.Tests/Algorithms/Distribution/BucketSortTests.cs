using Algorithms.Sorting.Algorithms.Distribution;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.Distribution;

/// <summary>
/// Tests for <see cref="BucketSort{TKey}"/>.
/// </summary>
public sealed class BucketSortTests : SortingAlgorithmTestBase<BucketSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BucketSort<>);

	/// <summary>
	/// Tests if the constructor of <see cref="BucketSort{TKey}"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidBuckets_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("buckets", () => new BucketSort<int>(0));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
