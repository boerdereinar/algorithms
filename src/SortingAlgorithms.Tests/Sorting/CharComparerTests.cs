using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Tests.Sorting;

/// <summary>
/// Tests for <see cref="CharComparer"/>.
/// </summary>
public class CharComparerTests
{
	/// <summary>
	/// Tests if <see cref="CharComparer.Compare(char,char)"/> returns the correct result.
	/// </summary>
	/// <param name="x">First value.</param>
	/// <param name="y">Second value.</param>
	/// <param name="expected">Expected value.</param>
	[Theory]
	[InlineData('a', 'a', 0)]
	[InlineData('a', 'b', -1)]
	[InlineData('b', 'a', 1)]
	public void Compare_ReturnsCorrectResult(char x, char y, int expected)
	{
		var comparer = CharComparer.FromStringComparer(Comparer<string>.Default);
		Assert.Equal(expected, comparer.Compare(x, y));
	}

	/// <summary>
	/// Tests if <see cref="CharComparer.Compare(char?,char?)"/> returns the correct result.
	/// </summary>
	/// <param name="x">First value.</param>
	/// <param name="y">Second value.</param>
	/// <param name="expected">Expected value.</param>
	[Theory]
	[InlineData('a', 'a', 0)]
	[InlineData('a', 'b', -1)]
	[InlineData('b', 'a', 1)]
	[InlineData(null, null, 0)]
	[InlineData(null, 'a', -1)]
	[InlineData('a', null, 1)]
	public void Compare_Nullable_ReturnsCorrectResult(char? x, char? y, int expected)
	{
		var comparer = CharComparer.FromStringComparer(Comparer<string>.Default);
		Assert.Equal(expected, comparer.Compare(x, y));
	}
}
