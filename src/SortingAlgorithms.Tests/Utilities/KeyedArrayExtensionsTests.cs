using SortingAlgorithms.Sorting;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Tests.Utilities;

/// <summary>
/// Extension methods for <see cref="KeyedArray{TElement,TKey}"/>.
/// </summary>
public sealed class KeyedArrayExtensionsTests
{
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
