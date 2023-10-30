using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms;

/// <summary>
/// Base class for slow sorting algorithm tests.
/// </summary>
public abstract class SlowSortingAlgorithmTestBase : SortingAlgorithmTestBase<SlowSortingAlgorithmTestBase>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	public static SortTheoryData<int> Integer(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<int>(),
			Enumerable.Range(0, 5),
			new[] { 4, 2, 1, 0, 3 },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<uint> UnsignedInteger(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<uint>(),
			Enumerable.Range(0, 5),
			new[] { 4, 2, 1, 0, 3 },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<double> Double(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<double>(),
			Enumerable.Range(0, 5),
			new[] { 4, 2, 1, 0, 3 },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<string> String(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<string>(),
			new[] { "a", "b", "c", "d", "e" },
			new[] { "e", "c", "b", "a", "d" },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<int> Composite(bool reverse)
	{
		var sorted = new[] { 0, 2, 4, 1, 3 };
		return new(reverse)
		{
			Enumerable.Empty<int>(),
			{ sorted, sorted },
			{ sorted, true },
		};
	}
}
