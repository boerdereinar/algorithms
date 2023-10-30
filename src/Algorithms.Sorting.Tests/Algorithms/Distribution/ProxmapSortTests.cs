using Algorithms.Sorting.Algorithms.Distribution;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.Distribution;

/// <summary>
/// Tests for <see cref="ProxmapSort{TKey}"/>.
/// </summary>
public sealed class ProxmapSortTests : SortingAlgorithmTestBase<ProxmapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(ProxmapSort<>);

	/// <summary>
	/// Tests if the constructor of <see cref="ProxmapSort{TKey}"/> throws an <see cref="ArgumentOutOfRangeException"/>.
	/// </summary>
	[Fact]
	public void Constructor_InvalidBuckets_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("buckets", () => new ProxmapSort<int>(0));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
