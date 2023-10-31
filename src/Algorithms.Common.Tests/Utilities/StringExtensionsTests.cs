using Algorithms.Common.Utilities;

namespace Algorithms.Common.Tests.Utilities;

/// <summary>
/// Tests for <see cref="StringExtensions"/>.
/// </summary>
public sealed class StringExtensionsTests
{
	private const string S = "Hello world";

	/// <summary>
	/// Tests if <see cref="StringExtensions.TryGetCharAt"/> returns the character at the given index.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <param name="c">The expected character.</param>
	[Theory]
	[InlineData(0, 'H')]
	[InlineData(1, 'e')]
	[InlineData(2, 'l')]
	[InlineData(3, 'l')]
	[InlineData(4, 'o')]
	[InlineData(5, ' ')]
	[InlineData(6, 'w')]
	[InlineData(7, 'o')]
	[InlineData(8, 'r')]
	[InlineData(9, 'l')]
	[InlineData(10, 'd')]
	public void TryGetCharAt_Index_ReturnsChar(int i, char c)
	{
		var result = S.TryGetCharAt(i);
		Assert.Equal(c, result);
	}

	/// <summary>
	/// Tests if <see cref="StringExtensions.TryGetCharAt"/> returns <c>null</c> if the index is out of bounds.
	/// </summary>
	/// <param name="i">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(11)]
	public void TryGetCharAt_IndexOutOfBounds_ReturnsNull(int i)
	{
		var result = S.TryGetCharAt(i);
		Assert.Null(result);
	}
}
