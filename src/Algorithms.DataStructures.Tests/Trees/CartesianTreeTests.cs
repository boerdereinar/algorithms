using Algorithms.DataStructures.Trees;

namespace Algorithms.DataStructures.Tests.Trees;

/// <summary>
/// Tests for <see cref="CartesianTree{TSource,TKey}"/>.
/// </summary>
public sealed class CartesianTreeTests : TraversableTreeTestBase<CartesianTree<int, int>, CartesianTree>
{
	/// <summary>
	/// Tests if <see cref="CartesianTree{TSource,TKey}.Create"/> creates a leaf node.
	/// </summary>
	[Fact]
	public void Create_Empty_CreatesLeafNode()
	{
		var tree = CartesianTree<int, int>.Create(Array.Empty<int>(), x => x, Comparer<int>.Default);
		Assert.Null(tree);
	}

	/// <summary>
	/// Tests if <see cref="CartesianTree{TSource,TKey}.Create"/> creates a single node.
	/// </summary>
	[Fact]
	public void Create_Single_CreatesSingleNode()
	{
		var tree = CartesianTree<int, int>.Create(new[] { 1 }, x => x, Comparer<int>.Default);
		Assert.NotNull(tree);
		Assert.Equal(1, tree.Value);
		Assert.Equal(1, tree.Key);
	}

	/// <summary>
	/// Tests if <see cref="CartesianTree{TSource,TKey}.Create"/> creates a tree.
	/// </summary>
	[Fact]
	public void Create_CreatesCorrectTree()
	{
		var tree = CartesianTree<int, int>.Create(new[] { 2, 1, 4, 3 }, x => x, Comparer<int>.Default);

		Assert.NotNull(tree);
		Assert.Equal(1, tree.Value);
		Assert.NotNull(tree.Left);
		Assert.Equal(2, tree.Left.Value);
		Assert.NotNull(tree.Right);
		Assert.Equal(3, tree.Right.Value);
		Assert.NotNull(tree.Right.Left);
		Assert.Equal(4, tree.Right.Left.Value);
	}
}
