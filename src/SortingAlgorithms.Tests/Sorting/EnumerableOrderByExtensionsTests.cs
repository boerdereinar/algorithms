using SortingAlgorithms.Algorithms;
using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Tests.Sorting;

/// <summary>
/// Tests for <see cref="EnumerableOrderByExtensions"/>.
/// </summary>
public sealed class EnumerableOrderByExtensionsTests
{
	private static readonly int[] _expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
	private static readonly int[] _shuffled = { 9, 3, 10, 8, 4, 1, 7, 2, 5, 6 };

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.Order{TSource, TAlgorithm}(IEnumerable{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void Order_TAlgorithm_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.Order<int, DefaultSort<int>>());
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.Order{TSource, TAlgorithm}(IEnumerable{TSource}, IComparer{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void Order_TAlgorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.Order<int, DefaultSort<int>>(Comparer<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderBy{TSource, TKey, TAlgorithm}(IEnumerable{TSource}, Func{TSource, TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderBy_TAlgorithm_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.OrderBy<int, int, DefaultSort<int>>(x => x));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderBy{TSource, TKey, TAlgorithm}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderBy_TAlgorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.OrderBy<int, int, DefaultSort<int>>(x => x, Comparer<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderDescending{TSource, TAlgorithm}(IEnumerable{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderDescending_TAlgorithm_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderDescending<int, DefaultSort<int>>());
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderDescending{TSource, TAlgorithm}(IEnumerable{TSource}, IComparer{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderDescending_TAlgorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderDescending<int, DefaultSort<int>>(Comparer<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderByDescending{TSource, TKey, TAlgorithm}(IEnumerable{TSource}, Func{TSource, TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderByDescending_TAlgorithm_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderByDescending<int, int, DefaultSort<int>>(x => x));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderByDescending{TSource, TKey, TAlgorithm}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderByDescending_TAlgorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderByDescending<int, int, DefaultSort<int>>(x => x, Comparer<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.Order{TSource}(IEnumerable{TSource}, ISortingAlgorithm{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void Order_Algorithm_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.Order(DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.Order{TSource}(IEnumerable{TSource}, IComparer{TSource}, ISortingAlgorithm{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void Order_Algorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.Order(Comparer<int>.Default, DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, ISortingAlgorithm{TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderBy_Algorithm_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.OrderBy(x => x, DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey}, ISortingAlgorithm{TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderBy_Algorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected, _shuffled.OrderBy(x => x, Comparer<int>.Default, DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderDescending{TSource}(IEnumerable{TSource}, ISortingAlgorithm{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderDescending_Algorithm_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderDescending(DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderDescending{TSource}(IEnumerable{TSource}, IComparer{TSource}, ISortingAlgorithm{TSource})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderDescending_Algorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderDescending(Comparer<int>.Default, DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderByDescending{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, ISortingAlgorithm{TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderByDescending_Algorithm_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderByDescending(x => x, DefaultSort<int>.Default));
	}

	/// <summary>
	/// Tests if <see cref="EnumerableOrderByExtensions.OrderByDescending{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey}, IComparer{TKey}, ISortingAlgorithm{TKey})"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderByDescending_Algorithm_WithComparer_SortsCorrectly()
	{
		Assert.Equal(_expected.Reverse(), _shuffled.OrderByDescending(x => x, Comparer<int>.Default, DefaultSort<int>.Default));
	}
}
