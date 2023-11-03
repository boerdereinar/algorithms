using Algorithms.Common.Tests.TestUtilities;
using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="BTreeSort{TKey}"/>.
/// </summary>
public sealed class BTreeSortTests : SortingAlgorithmTestBase<BTreeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BTreeSort<>);

	/// <summary>
	/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> if m is invalid.
	/// </summary>
	[Fact]
	public void Constructor_InvalidOrder_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("m", () => new BTreeSort<int>(0));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests for <see cref="BTreeSort{TKey}"/> with order 3.
	/// </summary>
	public sealed class BTreeWithOrder3SortTests : SortingAlgorithmTestBase<BTreeSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(BTreeSort<>);

		/// <inheritdoc />
		protected override object?[] Arguments => new object[] { 3 };
	}
}
