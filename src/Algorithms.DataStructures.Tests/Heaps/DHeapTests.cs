using Algorithms.Common.Tests.TestUtilities;
using Algorithms.DataStructures.Heaps;

namespace Algorithms.DataStructures.Tests.Heaps;

/// <summary>
/// Tests for <see cref="DHeap{TValue,TKey}"/>.
/// </summary>
public sealed class DHeapTests : HeapTestBase<DHeap<int, int>, DHeap>
{
	/// <summary>
	/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> if d is invalid.
	/// </summary>
	[Fact]
	public void Constructor_InvalidChildren_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("d", () => new DHeap<int, int>(1, Comparer<int>.Default));
		Assert.Equal(1, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> if d is invalid.
	/// </summary>
	[Fact]
	public void ConstructorArray_InvalidChildren_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("d", () => new DHeap<int, int>(1, Array.Empty<int>(), x => x, Comparer<int>.Default));
		Assert.Equal(1, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}
}
