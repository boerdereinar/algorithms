using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Tests.Utilities;

/// <summary>
/// Tests for <see cref="SearchOperations"/>.
/// </summary>
public sealed class SearchOperationsTests
{
	/// <summary>
	/// Tests if <see cref="SearchOperations.BinarySearch{TSource,TKey}"/> returns the correct index.
	/// </summary>
	/// <param name="x">The element.</param>
	/// <param name="expected">Expected index.</param>
	[Theory]
	[InlineData(0, ~0)]
	[InlineData(1, 0)]
	[InlineData(2, ~1)]
	[InlineData(3, 1)]
	[InlineData(4, ~2)]
	[InlineData(5, 2)]
	[InlineData(6, ~3)]
	[InlineData(7, 3)]
	[InlineData(8, ~4)]
	public void BinarySearch_ReturnsCorrectIndex(int x, int expected)
	{
		var source = new[] { 1, 3, 5, 7 };
		var actual = source.BinarySearch(x, a => a, Comparer<int>.Default);
		Assert.Equal(expected, actual);
	}
}
