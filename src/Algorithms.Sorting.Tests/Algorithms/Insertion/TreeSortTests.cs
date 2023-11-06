using Algorithms.DataStructures.Trees;
using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="TreeSort{TKey,TTree}"/>.
/// </summary>
public sealed class TreeSortTests
{
	/// <summary>
	/// Tests if <see cref="TreeSort{TKey,TTree}.Default"/> returns the correct instance.
	/// </summary>
	[Fact]
	public void Default_ReturnsDefaultInstance()
	{
		var instance = TreeSort<int, BinaryTree>.Default;
		Assert.IsType<TreeSort<int, BinaryTree>>(instance);
	}

	/// <summary>
	/// Tests if <see cref="TreeSort{TKey,TTree}.CreateComposite{T}"/> returns the correct composite.
	/// </summary>
	[Fact]
	public void CreateComposite_ReturnsComposite()
	{
		var instance = TreeSort<int, BinaryTree>.Default.CreateComposite<float>();
		Assert.IsType<TreeSort<float, BinaryTree>>(instance);
	}

	/// <summary>
	/// Tests for <see cref="TreeSort{TKey,TTree}"/>.
	/// </summary>
	public sealed class TreeSortWithArgumentTests
	{
		/// <summary>
		/// Tests if <see cref="TreeSort{TKey,TTree}.Default"/> returns the correct instance.
		/// </summary>
		[Fact]
		public void Default_ReturnsDefaultInstance()
		{
			var instance = TreeSort<int, BTree, int>.Default;
			Assert.IsType<TreeSort<int, BTree, int>>(instance);
		}

		/// <summary>
		/// Tests if <see cref="TreeSort{TKey,TTree}.CreateComposite{T}"/> returns the correct composite.
		/// </summary>
		[Fact]
		public void CreateComposite_WithoutArgument_ReturnsCompositeWithoutArgument()
		{
			var instance = TreeSort<int, BTree, int>.Default.CreateComposite<float>();
			Assert.IsType<TreeSort<float, BTree, int>>(instance);

			var typedInstance = (TreeSort<float, BTree, int>)instance;
			Assert.Equal(default, typedInstance.Argument);
			Assert.False(typedInstance.HasArgument);
		}

		/// <summary>
		/// Tests if <see cref="TreeSort{TKey,TTree}.CreateComposite{T}"/> returns the correct composite.
		/// </summary>
		[Fact]
		public void CreateComposite_WithArgument_ReturnsCompositeWithArgument()
		{
			var instance = new TreeSort<int, BTree, int>(5).CreateComposite<float>();
			Assert.IsType<TreeSort<float, BTree, int>>(instance);

			var typedInstance = (TreeSort<float, BTree, int>)instance;
			Assert.Equal(5, typedInstance.Argument);
			Assert.True(typedInstance.HasArgument);
		}
	}
}
