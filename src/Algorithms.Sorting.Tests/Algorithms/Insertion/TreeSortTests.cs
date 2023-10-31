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
	/// Tests for <see cref="BinaryTreeSort{TKey}"/>.
	/// </summary>
	public sealed class BinaryTreeSortTests : SortingAlgorithmTestBase<BinaryTreeSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(BinaryTreeSort<>);
	}

	/// <summary>
	/// Tests for <see cref="CartesianTreeSort{TKey}"/>.
	/// </summary>
	public sealed class CartesianTreeSortTests : SortingAlgorithmTestBase<CartesianTreeSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(CartesianTreeSort<>);
	}

	/// <summary>
	/// Tests for <see cref="RedBlackTreeSort{TKey}"/>.
	/// </summary>
	public sealed class RedBlackTreeSortTests : SortingAlgorithmTestBase<RedBlackTreeSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(RedBlackTreeSort<>);
	}

	/// <summary>
	/// Tests for <see cref="SplaySort{TKey}"/>.
	/// </summary>
	public sealed class SplaySortTests : SortingAlgorithmTestBase<SplaySortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(SplaySort<>);
	}
}
