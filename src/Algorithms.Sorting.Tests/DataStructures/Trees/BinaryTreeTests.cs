using Algorithms.Sorting.DataStructures.Trees;

namespace Algorithms.Sorting.Tests.DataStructures.Trees;

/// <summary>
/// Tests for <see cref="BinaryTree{TSource,TKey}"/>.
/// </summary>
public sealed class BinaryTreeTests : TraversableTreeTestBase<BinaryTree<int, int>, BinaryTree>
{
	/// <summary>
	/// Tests if the constructor of <see cref="BinaryTree{TSource,TKey}"/> creates a leaf node.
	/// </summary>
	[Fact]
	public void Constructor_CreatesNode()
	{
		var tree = new BinaryTree<int, int>(1, 1, Comparer<int>.Default);
		Assert.Equal(1, tree.Value);
		Assert.Equal(1, tree.Key);
		Assert.Null(tree.Left);
		Assert.Null(tree.Right);
	}

	/// <summary>
	/// Tests if <see cref="BinaryTree{TSource,TKey}.Insert"/> sets the left descendant.
	/// </summary>
	[Fact]
	public void Insert_LessThan_SetsLeft()
	{
		var tree = new BinaryTree<int, int>(0, 0, Comparer<int>.Default);

		tree.Insert(-1, -1);

		Assert.NotNull(tree.Left);
		Assert.Equal(-1, tree.Left.Value);
		Assert.Equal(-1, tree.Left.Key);
	}

	/// <summary>
	/// Tests if <see cref="BinaryTree{TSource,TKey}.Insert"/> sets the right descendant.
	/// </summary>
	[Fact]
	public void Insert_GreaterThan_SetsRight()
	{
		var tree = new BinaryTree<int, int>(0, 0, Comparer<int>.Default);
		tree.Insert(1, 1);

		Assert.NotNull(tree.Right);
		Assert.Equal(1, tree.Right.Value);
		Assert.Equal(1, tree.Right.Key);
	}

	/// <summary>
	/// Tests if <see cref="BinaryTree{TSource,TKey}.Insert"/> sets the right descendant.
	/// </summary>
	[Fact]
	public void Insert_EqualTo_SetsRight()
	{
		var tree = new BinaryTree<int, int>(0, 0, Comparer<int>.Default);
		tree.Insert(0, 0);

		Assert.NotNull(tree.Right);
		Assert.Equal(0, tree.Right.Value);
		Assert.Equal(0, tree.Right.Key);
	}
}
