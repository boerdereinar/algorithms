namespace Algorithms.Common.Comparers;

/// <summary>
/// Reverse <see cref="IComparer{T}"/>.
/// </summary>
/// <typeparam name="T">The type of objects to compare.</typeparam>
public sealed class ReverseComparer<T> : IComparer<T>
{
	private readonly IComparer<T> _comparer;

	/// <summary>
	/// Initializes a new instance of the <see cref="ReverseComparer{T}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer to reverse.</param>
	public ReverseComparer(IComparer<T> comparer)
	{
		_comparer = comparer;
	}

	/// <inheritdoc/>
	public int Compare(T? x, T? y)
	{
		return -_comparer.Compare(x, y);
	}
}

/// <summary>
/// Extensions methods for <see cref="IComparer{T}"/>.
/// </summary>
public static class ComparerExtensions
{
	/// <summary>
	/// Reverses the output of a <see cref="IComparer{T}"/>.
	/// </summary>
	/// <param name="comparer">The comparer to reverse.</param>
	/// <typeparam name="T">The type of objects to compare.</typeparam>
	/// <returns>The reversed <see cref="IComparer{T}"/>.</returns>
	public static IComparer<T> Reverse<T>(this IComparer<T> comparer)
	{
		return new ReverseComparer<T>(comparer);
	}
}
