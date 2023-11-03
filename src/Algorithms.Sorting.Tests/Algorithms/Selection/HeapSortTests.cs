using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="HeapSort{TKey,THeap}"/>.
/// </summary>
public sealed class HeapSortTests
{
	/// <summary>
	/// Tests if <see cref="HeapSort{TKey,THeap}.Default"/> returns the correct instance.
	/// </summary>
	[Fact]
	public void Default_ReturnsDefaultInstance()
	{
		var instance = HeapSort<int, DefaultHeap>.Default;
		Assert.IsType<HeapSort<int, DefaultHeap>>(instance);
	}
}
