using Algorithms.Sorting.DataStructures.Trees;

namespace Algorithms.Sorting.Tests.DataStructures.Trees;

/// <summary>
/// Base class for traversable tree tests.
/// </summary>
/// <typeparam name="TTree">The type of the tree.</typeparam>
/// <typeparam name="TTreeFactory">The type of the tree factory.</typeparam>
public abstract class TraversableTreeTestBase<TTree, TTreeFactory>
	where TTree : ITraversableTree<int, int>
	where TTreeFactory : ITraversableTree
{
	/// <summary>
	/// Tests if enumerating <see cref="ITraversableTree{TSource,TKey}"/> returns a sorted collection.
	/// </summary>
	[Fact]
	public void GetEnumerator_ReturnsSorted()
	{
		var random = new Random(42);
		var tree = TTreeFactory.Create(Enumerable.Range(0, 100).OrderBy(_ => random.Next()), x => x, Comparer<int>.Default);
		Assert.Equal(Enumerable.Range(0, 100), tree);
	}

	/// <summary>
	/// Tests if <see cref="ITraversableTree{TSource,TKey}.Create"/> retuns null.
	/// </summary>
	[Fact]
	public void Create_Empty_ReturnsNull()
	{
		var tree = TTreeFactory.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		Assert.Null(tree);
	}

	/// <summary>
	/// Tests if <see cref="ITraversableTree{TSource,TKey}.Create"/> creates a tree.
	/// </summary>
	[Fact]
	public void Create_CreatesTree()
	{
		var tree = TTree.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.IsType<TTree>(tree);
	}

	/// <summary>
	/// Tests if <see cref="ITraversableTree.Create{TSource,TKey}"/> creates a tree.
	/// </summary>
	[Fact]
	public void FactoryCreate_CreatesTree()
	{
		var tree = TTreeFactory.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.IsType<TTree>(tree);
	}
}
