using SortingAlgorithms.Algorithms.DistributionSorts;
using SortingAlgorithms.Tests.TestUtilities;

namespace SortingAlgorithms.Tests.Algorithms.DistributionSorts;

/// <summary>
/// Tests for <see cref="FlashSort{TKey}"/>.
/// </summary>
public sealed class FlashSortTests : SortingAlgorithmTestBase<FlashSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(FlashSort<>);

	/// <summary>
	/// Tests if the constructor of <see cref="FlashSort{TKey}"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidBuckets_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("buckets", () => new FlashSort<int>(0));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
