using Algorithms.Sorting.DataStructures.Trees;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.DataStructures.Trees;

/// <summary>
/// Tests for <see cref="BinomialTree{TSource,TKey}"/>.
/// </summary>
public sealed class BinomialTreeTests
{
	/// <summary>
	/// Tests if the constructor creates a tree with order 0.
	/// </summary>
	[Fact]
	public void Constructor_CreatesZeroOrderTree()
	{
		var tree = new BinomialTree<int, int>(1, 1);
		Assert.Equal(1, tree.Value);
		Assert.Equal(1, tree.Key);
		Assert.Equal(0, tree.Order);
	}

	/// <summary>
	/// Tests if <see cref="BinomialTree{TSource,TKey}.Merge"/> throws an exception if the trees have different orders.
	/// </summary>
	[Fact]
	public void Merge_DifferentOrders_ThrowsArgumentException()
	{
		var left = new BinomialTree<int, int>(1, 1);
		var right = BinomialTree<int, int>.Merge(new(2, 2), new(3, 3));

		var ex = Assert.Throws<ArgumentException>("right", () => BinomialTree<int, int>.Merge(left, right));
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="BinomialTree{TSource,TKey}.Merge"/> merges into the left tree.
	/// </summary>
	[Fact]
	public void Merge_LeftEqualToRight_MergesIntoLeft()
	{
		var left = new BinomialTree<int, int>(1, 1);
		var right = new BinomialTree<int, int>(1, 1);

		var actual = BinomialTree<int, int>.Merge(left, right);
		Assert.Same(left, actual);
		Assert.Equal(left.Value, actual.Value);
		Assert.Equal(left.Key, actual.Key);
		Assert.Equal(1, actual.Order);
		Assert.Equal(right.Value, actual.Forest[0].Value);
	}

	/// <summary>
	/// Tests if <see cref="BinomialTree{TSource,TKey}.Merge"/> merges into the left tree.
	/// </summary>
	[Fact]
	public void Merge_LeftLessThanRight_MergesIntoLeft()
	{
		var left = new BinomialTree<int, int>(1, 1);
		var right = new BinomialTree<int, int>(2, 2);

		var actual = BinomialTree<int, int>.Merge(left, right);
		Assert.Equal(left.Value, actual.Value);
		Assert.Equal(left.Key, actual.Key);
		Assert.Equal(1, actual.Order);
		Assert.Equal(right.Value, actual.Forest[0].Value);
	}

	/// <summary>
	/// Tests if <see cref="BinomialTree{TSource,TKey}.Merge"/> merges into the right tree.
	/// </summary>
	[Fact]
	public void Merge_RightLessThanLeft_MergesIntoRight()
	{
		var left = new BinomialTree<int, int>(2, 2);
		var right = new BinomialTree<int, int>(1, 1);

		var actual = BinomialTree<int, int>.Merge(left, right);
		Assert.Equal(right.Value, actual.Value);
		Assert.Equal(right.Key, actual.Key);
		Assert.Equal(1, actual.Order);
		Assert.Equal(left.Value, actual.Forest[0].Value);
	}
}
