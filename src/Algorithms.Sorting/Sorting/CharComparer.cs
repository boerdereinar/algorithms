namespace Algorithms.Sorting.Sorting;

/// <summary>
/// Comparer for <see cref="char"/>.
/// </summary>
internal class CharComparer : Comparer<char?>, IComparer<char>
{
	private readonly IComparer<string> _comparer;

	private CharComparer(IComparer<string> comparer)
	{
		_comparer = comparer;
	}

	/// <inheritdoc/>
	public override int Compare(char? x, char? y)
	{
		return _comparer.Compare(x?.ToString(), y?.ToString());
	}

	/// <inheritdoc />
	public int Compare(char x, char y)
	{
		return _comparer.Compare(x.ToString(), y.ToString());
	}

	/// <summary>
	/// Creates a char comparer from a string comparer.
	/// </summary>
	/// <param name="comparer">The string comparer to convert to a char comparer.</param>
	/// <returns>The char comparer.</returns>
	public static CharComparer FromStringComparer(IComparer<string> comparer)
	{
		return new(comparer);
	}
}
