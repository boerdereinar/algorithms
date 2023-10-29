using SortingAlgorithms.Sorting;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Tests.Utilities;

/// <summary>
/// Extension methods for <see cref="KeyedArray{TElement,TKey}"/>.
/// </summary>
public sealed class KeyedArrayExtensionsTests
{
	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.IsSorted{TSource,TKey}"/> returns <c>true</c> if the input is sorted.
	/// </summary>
	/// <param name="source">Sorted array.</param>
	[Theory]
	[InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
	[InlineData(new[] { 1, 1, 2, 2, 3, 3 })]
	[InlineData(new[] { -6, -5, -4, -3, -2, -1 })]
	[InlineData(new int[0])]
	public void IsSorted_Sorted_ReturnsTrue(int[] source)
	{
		var keyedArray = new KeyedArray<int, int>(source, x => x, Comparer<int>.Default);
		Assert.True(keyedArray.IsSorted());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.IsSorted{TSource,TKey}"/> returns <c>false</c> if the input is unsorted.
	/// </summary>
	/// <param name="source">Sorted array.</param>
	[Theory]
	[InlineData(new[] { 6, 5, 4, 3, 2, 1 })]
	[InlineData(new[] { 6, 6, 5, 5, 4, 4 })]
	[InlineData(new[] { -1, -2, -3, -4, -5, -6 })]
	public void IsSorted_Unsorted_ReturnsFalse(int[] source)
	{
		var keyedArray = new KeyedArray<int, int>(source, x => x, Comparer<int>.Default);
		Assert.False(keyedArray.IsSorted());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.IsSorted{TSource,TKey}"/> returns <c>true</c> if the input is sorted.
	/// </summary>
	/// <param name="source">Sorted array.</param>
	[Theory]
	[InlineData(new[] { 6, 5, 4, 3, 2, 1 })]
	[InlineData(new[] { 6, 6, 5, 5, 4, 4 })]
	[InlineData(new[] { -1, -2, -3, -4, -5, -6 })]
	public void IsSorted_ReverseSorted_ReturnsTrue(int[] source)
	{
		var keyedArray = new KeyedArray<int, int>(source, x => x, Comparer<int>.Default.Reverse());
		Assert.True(keyedArray.IsSorted());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.IsSorted{TSource,TKey}"/> returns <c>false</c> if the input is unsorted.
	/// </summary>
	/// <param name="source">Sorted array.</param>
	[Theory]
	[InlineData(new[] { 1, 2, 3, 4, 5, 6 })]
	[InlineData(new[] { 1, 1, 2, 2, 3, 3 })]
	[InlineData(new[] { -6, -5, -4, -3, -2, -1 })]
	public void IsSorted_ReverseUnsorted_ReturnsFalse(int[] source)
	{
		var keyedArray = new KeyedArray<int, int>(source, x => x, Comparer<int>.Default.Reverse());
		Assert.False(keyedArray.IsSorted());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.MinMax{TSource,TKey}(KeyedArray{TSource,TKey})"/> returns the minimum and maximum.
	/// </summary>
	/// <param name="source">Source to check.</param>
	/// <param name="min">Expected minimum.</param>
	/// <param name="max">Expected maximum.</param>
	[Theory]
	[InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 1, 6)]
	[InlineData(new[] { 1, 1, 1, 1, 1, 1 }, 1, 1)]
	[InlineData(new[] { -1, -2, -3, -4, -5, -6 }, -6, -1)]
	public void MinMaxKeyedArray_ReturnsMinMax(int[] source, int min, int max)
	{
		var actual = source.ToKeyedArray(x => x, Comparer<int>.Default).MinMax();
		Assert.NotNull(actual);
		Assert.Equal(min, actual.Value.Min);
		Assert.Equal(max, actual.Value.Max);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.MinMax{TSource,TKey}(KeyedArray{TSource,TKey})"/> returns the reversed minimum and maximum.
	/// </summary>
	/// <param name="source">Source to check.</param>
	/// <param name="min">Expected minimum.</param>
	/// <param name="max">Expected maximum.</param>
	[Theory]
	[InlineData(new[] { 1, 2, 3, 4, 5, 6 }, 6, 1)]
	[InlineData(new[] { 1, 1, 1, 1, 1, 1 }, 1, 1)]
	[InlineData(new[] { -1, -2, -3, -4, -5, -6 }, -1, -6)]
	public void MinMaxKeyedArray_Reversed_ReturnsMinMax(int[] source, int min, int max)
	{
		var actual = source.ToKeyedArray(x => x, Comparer<int>.Default.Reverse()).MinMax();
		Assert.NotNull(actual);
		Assert.Equal(min, actual.Value.Min);
		Assert.Equal(max, actual.Value.Max);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayExtensions.MinMax{TSource,TKey}(KeyedArray{TSource,TKey})"/> returns null.
	/// </summary>
	[Fact]
	public void MinMaxKeyedArray_Empty_ReturnsNull()
	{
		var actual = new KeyedArray<int, int>(0, Comparer<int>.Default).MinMax();
		Assert.Null(actual);
	}
}
