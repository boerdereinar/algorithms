using Algorithms.Sorting.Algorithms.DistributionSorts;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.DistributionSorts;

/// <summary>
/// Tests for <see cref="BurstSort"/>.
/// </summary>
public sealed class BurstSortTests : SortingAlgorithmTestBase<BurstSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BurstSort);

	/// <summary>
	/// Tests if the constructor of <see cref="BurstSort"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidInitialSize_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("initialSize", () => new BurstSort(0, 4, 32));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the constructor of <see cref="BurstSort"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidGrowthFactor_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("growthFactor", () => new BurstSort(4, 1, 32));
		Assert.Equal(1, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the constructor of <see cref="BurstSort"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidThreshold_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("threshold", () => new BurstSort(4, 4, 3));
		Assert.Equal(3, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
