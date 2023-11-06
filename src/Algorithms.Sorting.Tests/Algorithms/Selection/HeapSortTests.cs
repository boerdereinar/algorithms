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

	/// <summary>
	/// Tests if <see cref="HeapSort{TKey,THeap}.CreateComposite{T}"/> returns the correct composite.
	/// </summary>
	[Fact]
	public void CreateComposite_ReturnsComposite()
	{
		var instance = HeapSort<int, DefaultHeap>.Default.CreateComposite<float>();
		Assert.IsType<HeapSort<float, DefaultHeap>>(instance);
	}

	/// <summary>
	/// Tests for <see cref="HeapSort{TKey,THeap}"/>.
	/// </summary>
	public sealed class HeapSortWithArgumentTests
	{
		/// <summary>
		/// Tests if <see cref="HeapSort{TKey,THeap}.Default"/> returns the correct instance.
		/// </summary>
		[Fact]
		public void Default_ReturnsDefaultInstance()
		{
			var instance = HeapSort<int, DHeap, int>.Default;
			Assert.IsType<HeapSort<int, DHeap, int>>(instance);
		}

		/// <summary>
		/// Tests if <see cref="HeapSort{TKey,THeap}.CreateComposite{T}"/> returns the correct composite.
		/// </summary>
		[Fact]
		public void CreateComposite_WithoutArgument_ReturnsCompositeWithoutArgument()
		{
			var instance = HeapSort<int, DHeap, int>.Default.CreateComposite<float>();
			Assert.IsType<HeapSort<float, DHeap, int>>(instance);

			var typedInstance = (HeapSort<float, DHeap, int>)instance;
			Assert.Equal(default, typedInstance.Argument);
			Assert.False(typedInstance.HasArgument);
		}

		/// <summary>
		/// Tests if <see cref="HeapSort{TKey,THeap}.CreateComposite{T}"/> returns the correct composite.
		/// </summary>
		[Fact]
		public void CreateComposite_WithArgument_ReturnsCompositeWithArgument()
		{
			var instance = new HeapSort<int, DHeap, int>(5).CreateComposite<float>();
			Assert.IsType<HeapSort<float, DHeap, int>>(instance);

			var typedInstance = (HeapSort<float, DHeap, int>)instance;
			Assert.Equal(5, typedInstance.Argument);
			Assert.True(typedInstance.HasArgument);
		}
	}
}
