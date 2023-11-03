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
}
