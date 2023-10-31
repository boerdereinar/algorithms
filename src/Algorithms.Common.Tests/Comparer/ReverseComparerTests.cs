using Algorithms.Common.Comparers;

namespace Algorithms.Common.Tests.Comparer;

/// <summary>
/// Tests for <see cref="ReverseComparer{T}"/>.
/// </summary>
public sealed class ReverseComparerTests
{
	/// <summary>
	/// Tests if <see cref="ReverseComparer{T}.Compare"/> returns the reversed value of <see cref="Comparer{T}.Compare"/>.
	/// </summary>
	/// <param name="x">First value.</param>
	/// <param name="y">Second value.</param>
	/// <param name="expected">Expected value.</param>
	[Theory]
	[InlineData(0, 1, 1)]
	[InlineData(1, 0, -1)]
	[InlineData(0, 0, 0)]
	public void Compare_ReturnsReverse(int x, int y, int expected)
	{
		var comparer = new ReverseComparer<int>(Comparer<int>.Default);
		var actual = comparer.Compare(x, y);
		Assert.Equal(expected, actual);
	}
}
