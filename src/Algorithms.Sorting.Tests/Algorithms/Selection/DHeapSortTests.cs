using Algorithms.Common.Tests.TestUtilities;
using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="DHeapSort{TKey}"/>.
/// </summary>
public sealed class DHeapSortTests : SortingAlgorithmTestBase<DHeapSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(DHeapSort<>);

	/// <summary>
	/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> if d is invalid.
	/// </summary>
	[Fact]
	public void Constructor_InvalidChildren_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("d", () => new DHeapSort<int>(1));
		Assert.Equal(1, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests for <see cref="DHeapSort{TKey}"/> with 3 children.
	/// </summary>
	public sealed class DHeapSortWith3ChildrenTests : SortingAlgorithmTestBase<DHeapSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(DHeapSort<>);

		/// <inheritdoc />
		protected override object?[] Arguments => new object[] { 3 };
	}
}
