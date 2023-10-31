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
	/// Tests for <see cref="DefaultHeapSort{TKey}"/>.
	/// </summary>
	public sealed class DefaultHeapSortTests : SortingAlgorithmTestBase<DefaultHeapSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(DefaultHeapSort<>);
	}

	/// <summary>
	/// Tests for <see cref="BinaryHeapSort{TKey}"/>.
	/// </summary>
	public sealed class BinaryHeapSortTests : SortingAlgorithmTestBase<BinaryHeapSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(BinaryHeapSort<>);
	}

	/// <summary>
	/// Tests for <see cref="BinomialHeapSort{TKey}"/>.
	/// </summary>
	public sealed class BinomialHeapSortTests : SortingAlgorithmTestBase<BinomialHeapSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(BinomialHeapSort<>);
	}

	/// <summary>
	/// Tests for <see cref="LeftistHeapSort{TKey}"/>.
	/// </summary>
	public sealed class LeftistHeapSortTests : SortingAlgorithmTestBase<LeftistHeapSortTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(LeftistHeapSort<>);
	}
}
