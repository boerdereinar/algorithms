using Algorithms.DataStructures.Heaps;

namespace Algorithms.DataStructures.Tests.Heaps;

/// <summary>
/// Base class for heap tests.
/// </summary>
/// <typeparam name="THeap">The type of the heap.</typeparam>
/// <typeparam name="THeapFactory">The type of the heap factory.</typeparam>
public abstract class HeapTestBase<THeap, THeapFactory>
	where THeap : class, IHeap<int, int>
	where THeapFactory : IHeap
{
	/// <summary>
	/// Tests if the constructor creates an empty heap.
	/// </summary>
	[Fact]
	public void Constructor_CreatesEmptyHeap()
	{
		var heap = Activator.CreateInstance(typeof(THeap), Comparer<int>.Default) as THeap;
		Assert.NotNull(heap);
		Assert.Equal(0, heap.Count);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.Min"/> throws an <see cref="InvalidOperationException"/> if the heap is empty.
	/// </summary>
	[Fact]
	public void Min_Empty_ThrowsInvalidOperationException()
	{
		var heap = THeap.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		var ex = Assert.Throws<InvalidOperationException>(() => heap.Min);
		Assert.NotEmpty(ex.Message);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.Min"/> returns the minimum value in the heap if the heap is not empty.
	/// </summary>
	[Fact]
	public void Min_NotEmpty_ReturnsMin()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.Equal(1, heap.Min);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.Insert"/> inserts a value into the heap.
	/// </summary>
	[Fact]
	public void Insert_InsertsValue()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		heap.Insert(6, 6);
		Assert.Equal(6, heap.Count);
		Assert.Equal(1, heap.Min);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.Insert"/> inserts a value into the empty heap.
	/// </summary>
	[Fact]
	public void Insert_EmptyHeap_InsertsValue()
	{
		var heap = THeap.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		heap.Insert(1, 1);
		Assert.Equal(1, heap.Count);
		Assert.Equal(1, heap.Min);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.Insert"/> inserts a new minimum into the heap.
	/// </summary>
	[Fact]
	public void Insert_NewMin_UpdatesMin()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		heap.Insert(0, 0);
		Assert.Equal(6, heap.Count);
		Assert.Equal(0, heap.Min);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.TryGetMin"/> returns <see langword="false"/> if the heap is empty.
	/// </summary>
	[Fact]
	public void TryGetMin_Empty_ReturnsFalse()
	{
		var heap = THeap.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		Assert.False(heap.TryGetMin(out _, out _));
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.TryGetMin"/> returns <see langword="true"/> if the heap is not empty.
	/// </summary>
	[Fact]
	public void TryGetMin_NotEmpty_ReturnsTrue()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.True(heap.TryGetMin(out var value, out var key));
		Assert.Equal(1, value);
		Assert.Equal(1, key);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.DeleteMin"/> throws an <see cref="InvalidOperationException"/> if the heap is empty.
	/// </summary>
	[Fact]
	public void DeleteMin_Empty_ThrowsInvalidOperationException()
	{
		var heap = THeap.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		var ex = Assert.Throws<InvalidOperationException>(() => heap.DeleteMin());
		Assert.NotEmpty(ex.Message);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.DeleteMin"/> deletes the minimum value from the heap if the heap is not empty.
	/// </summary>
	[Fact]
	public void DeleteMin_NotEmpty_DeletesMin()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.Equal(5, heap.Count);
		Assert.Equal(1, heap.DeleteMin());
		Assert.Equal(4, heap.Count);
		Assert.Equal(2, heap.Min);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.TryDeleteMin"/> returns <see langword="false"/> if the heap is empty.
	/// </summary>
	[Fact]
	public void TryDeleteMin_Empty_ReturnsFalse()
	{
		var heap = THeap.Create(Enumerable.Empty<int>(), x => x, Comparer<int>.Default);
		Assert.False(heap.TryDeleteMin(out var value, out var key));
		Assert.Equal(default, value);
		Assert.Equal(default, key);
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.TryDeleteMin"/> returns <see langword="true"/> if the heap is not empty.
	/// </summary>
	[Fact]
	public void TryDeleteMin_NotEmpty_ReturnsTrue()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.Equal(5, heap.Count);
		Assert.True(heap.TryDeleteMin(out var value, out var key));
		Assert.Equal(4, heap.Count);
		Assert.Equal(1, value);
		Assert.Equal(1, key);
	}

	/// <summary>
	/// Tests if calling <see cref="IHeap{TSource,TKey}.TryDeleteMin"/> multiple times returns the ordered collection.
	/// </summary>
	[Fact]
	public void TryDeleteMin_ReturnsOrdered()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.Equal(5, heap.Count);

		for (var i = 1; i <= 5; i++)
		{
			Assert.True(heap.TryDeleteMin(out var value, out var key));
			Assert.Equal(i, value);
			Assert.Equal(i, key);
			Assert.Equal(5 - i, heap.Count);
		}

		Assert.False(heap.TryDeleteMin(out _, out _));
	}

	/// <summary>
	/// Tests if <see cref="IHeap{TSource,TKey}.Create"/> creates a heap.
	/// </summary>
	[Fact]
	public void Create_CreatesHeap()
	{
		var heap = THeap.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.IsType<THeap>(heap);
		Assert.Equal(5, heap.Count);
	}

	/// <summary>
	/// Tests if <see cref="IHeap.Create{TSource,TKey}"/> creates a heap.
	/// </summary>
	[Fact]
	public void FactoryCreate_CreatesHeap()
	{
		var heap = THeapFactory.Create(new[] { 5, 4, 3, 2, 1 }, x => x, Comparer<int>.Default);
		Assert.IsType<THeap>(heap);
		Assert.Equal(5, heap.Count);
	}
}
