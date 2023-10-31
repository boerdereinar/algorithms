using System.Diagnostics.Contracts;

namespace Algorithms.Common.Utilities;

/// <summary>
/// Extension methods for <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
	/// <summary>
	/// Tries to get the character at <paramref name="index"/> in <paramref name="s"/>.
	/// </summary>
	/// <param name="s">The string.</param>
	/// <param name="index">The index of the character.</param>
	/// <returns>
	/// The char at <paramref name="index"/> in <paramref name="s"/> if the index is in the bounds of the string.
	/// </returns>
	[Pure]
	public static char? TryGetCharAt(this string s, int index)
	{
		return s.Length > index && index >= 0 ? s[index] : null;
	}
}
