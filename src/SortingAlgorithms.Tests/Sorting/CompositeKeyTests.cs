using SortingAlgorithms.Sorting;

namespace SortingAlgorithms.Tests.Sorting;

/// <summary>
/// Tests for <see cref="CompositeKey{TPrimary,TSecondary}"/>.
/// </summary>
public sealed class CompositeKeyTests
{
	/// <summary>
	/// Gets the test data.
	/// </summary>
	public static TheoryData<(int Primary, int Secondary)?, (int Primary, int Secondary)?, int> Data => new()
	{
		{ (0, 0), (0, 0), 0 },
		{ (1, 0), (0, 0), 1 },
		{ (0, 0), (1, 0), -1 },
		{ (0, 1), (0, 0), 1 },
		{ (0, 0), (0, 1), -1 },
		{ null, null, 0 },
		{ (0, 0), null, 1 },
		{ null, (0, 0), -1 },
	};

	/// <summary>
	/// Tests if <see cref="CompositeKey{TPrimary,TSecondary}.Comparer.Compare"/> returns the correct result.
	/// </summary>
	/// <param name="x">First key.</param>
	/// <param name="y">Second key.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[MemberData(nameof(Data))]
	public void Compare_ReturnsCorrectResult((int Primary, int Secondary)? x, (int Primary, int Secondary)? y, int expected)
	{
		var a = x is var (xPrim, xSec) ? new CompositeKey<int, int>(xPrim, xSec) : null;
		var b = y is var (yPrim, ySec) ? new CompositeKey<int, int>(yPrim, ySec) : null;

		var comparer = new CompositeKey<int, int>.Comparer(Comparer<int>.Default, Comparer<int>.Default);
		var actual = comparer.Compare(a, b);
		Assert.Equal(expected, actual);
	}
}
