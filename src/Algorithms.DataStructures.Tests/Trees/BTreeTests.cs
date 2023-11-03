using Algorithms.Common.Tests.TestUtilities;
using Algorithms.DataStructures.Trees;

namespace Algorithms.DataStructures.Tests.Trees;

/// <summary>
/// Tests for <see cref="BTree{TSource,TKey}"/>.
/// </summary>
public sealed class BTreeTests : TraversableTreeTestBase<BTree<int, int>, BTree>
{
	/// <summary>
	/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> if m is invalid.
	/// </summary>
	[Fact]
	public void Constructor_InvalidOrder_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("m", () => new BTree<int, int>(0, Comparer<int>.Default));
		Assert.Equal(0, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="BTree{TValue,TKey}.Insert"/> inserts a value in a non full leaf node.
	/// </summary>
	[Fact]
	public void Insert_Leaf_NonFull_Inserts()
	{
		var node = new BTree<int, int>(Comparer<int>.Default);

		var inserted = node.Insert(1, 1);
		Assert.Same(node, inserted);
		Assert.Equal(new[] { 1 }, node.Values);
		Assert.Equal(new[] { 1 }, node.Keys);
		Assert.Equal(0, node.Children.Length);

		inserted = node.Insert(2, 2);
		Assert.Same(node, inserted);
		Assert.Equal(new[] { 1, 2 }, node.Values);
		Assert.Equal(new[] { 1, 2 }, node.Keys);
		Assert.Equal(0, node.Children.Length);
	}
}
