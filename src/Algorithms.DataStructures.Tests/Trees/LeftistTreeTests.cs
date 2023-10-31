using Algorithms.DataStructures.Trees;

namespace Algorithms.DataStructures.Tests.Trees;

/// <summary>
/// Tests for <see cref="LeftistTree{TSource,TKey}"/>.
/// </summary>
public sealed class LeftistTreeTests
{
	/// <summary>
	/// Tests if the constructor of <see cref="LeftistTree{TSource,TKey}"/> creates a node with a value.
	/// </summary>
	[Fact]
	public void Constructor_CreatesNode()
	{
		var tree = new LeftistTree<int, int>(1, 1, Comparer<int>.Default);
		IsNode(tree, 1, 1, 1);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Create"/> creates a leaf node.
	/// </summary>
	[Fact]
	public void Create_Empty_CreatesLeafNode()
	{
		var tree = LeftistTree<int, int>.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		Assert.Null(tree);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Create"/> creates a single node.
	/// </summary>
	[Fact]
	public void Create_Single_CreatesSingleNode()
	{
		var tree = LeftistTree<int, int>.Create(new[] { 1 }, x => x, Comparer<int>.Default);
		IsNode(tree, 1, 1, 1, true);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Create"/> creates a tree.
	/// </summary>
	[Fact]
	public void Create_CreatesTree()
	{
		var tree = LeftistTree<int, int>.Create(new[] { 1, 2, 3, 4, 5 }, x => x, Comparer<int>.Default);
		IsNode(tree, 1, 1, 2);
		IsNode(tree?.Left, 3, 3, 2);
		IsNode(tree?.Left?.Left, 4, 4, 1, true);
		IsNode(tree?.Left?.Right, 5, 5, 1, true);
		IsNode(tree?.Right, 2, 2, 1, true);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Merge"/> returns the left tree if the right tree is a leaf.
	/// </summary>
	[Fact]
	public void Merge_RightIsLeaf_ReturnsLeft()
	{
		var left = Node(1, 1);
		var actual = LeftistTree<int, int>.Merge(left, null);
		Assert.Same(left, actual);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Merge"/> returns the right tree if the left tree is a leaf.
	/// </summary>
	[Fact]
	public void Merge_LeftIsLeaf_ReturnsRight()
	{
		var right = Node(1, 1);
		var actual = LeftistTree<int, int>.Merge(null, right);
		Assert.Same(right, actual);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Merge"/> merges into the left tree.
	/// </summary>
	[Fact]
	public void Merge_LeftLessThanRight_MergesIntoLeft()
	{
		var left = Node(1, 1);
		var right = Node(2, 2);

		var actual = LeftistTree<int, int>.Merge(left, right);

		Assert.Same(left, actual);
		Assert.Same(right, left.Left);
		Assert.Null(left.Right);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Merge"/> merges into the right tree.
	/// </summary>
	[Fact]
	public void Merge_RightLessThanLeft_MergesIntoRight()
	{
		var left = Node(2, 2);
		var right = Node(1, 1);

		var actual = LeftistTree<int, int>.Merge(left, right);

		Assert.Same(right, actual);
		Assert.Same(left, right.Left);
		Assert.Null(right.Right);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Merge"/> sets the right child of the left tree if the left tree
	/// has a left child.
	/// </summary>
	[Fact]
	public void Merge_LeftHasLeftChild_SetsRightChild()
	{
		var left = LeftistTree<int, int>.Merge(Node(1, 1), Node(2, 2));
		var right = Node(3, 3);

		var actual = LeftistTree<int, int>.Merge(left, right);

		Assert.Same(left, actual);
		Assert.Same(right, left.Right);
		Assert.Equal(2, left.Rank);
	}

	/// <summary>
	/// Tests if <see cref="LeftistTree{TSource,TKey}.Merge"/> sets the left child of the left tree to the right tree
	/// and switches the left child of the left tree to the right child.
	/// </summary>
	[Fact]
	public void Merge_RightRankGreaterThanLeft_SwapsLeftAndRight()
	{
		var l1 = Node(1, 1);
		var l2 = Node(2, 2);
		var left = LeftistTree<int, int>.Merge(l1, l2);
		var r1 = Node(3, 3);
		var r2 = Node(4, 4);
		var r3 = Node(5, 5);
		var right = LeftistTree<int, int>.Merge(LeftistTree<int, int>.Merge(r1, r2), r3);

		var actual = LeftistTree<int, int>.Merge(left, right);

		Assert.Same(left, actual);
		Assert.Equal(2, left.Rank);
		Assert.Same(right, left.Left);
		Assert.Same(l2, left.Right);
	}

	private static LeftistTree<int, int> Node(int value, int key)
	{
		return new(value, key, Comparer<int>.Default);
	}

	private static void IsNode(LeftistTree<int, int>? node, int value, int key, int rank, bool leaf = false)
	{
		Assert.NotNull(node);
		Assert.Equal(value, node.Value);
		Assert.Equal(key, node.Key);
		Assert.Equal(rank, node.Rank);

		if (leaf)
		{
			Assert.Null(node.Left);
			Assert.Null(node.Right);
		}
	}
}
