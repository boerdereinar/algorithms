using SortingAlgorithms.Algorithms.ImpracticalSorts;
using SortingAlgorithms.Tests.TestUtilities;

namespace SortingAlgorithms.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="StalinSort{TKey}"/>.
/// </summary>
public sealed class StalinSortTests : SortingAlgorithmTestBase<StalinSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(StalinSort<>);

	/// <inheritdoc />
	public static SortTheoryData<int> Integer(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<int>(),
			{ Enumerable.Range(0, 100), reverse ? new[] { 0 } : Enumerable.Range(0, 100) },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<uint> UnsignedInteger(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<uint>(),
			{ Enumerable.Range(0, 100), reverse ? new[] { 0 } : Enumerable.Range(0, 100) },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<double> Double(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<double>(),
			{ Enumerable.Range(0, 100), reverse ? new[] { 0 } : Enumerable.Range(0, 100) },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<string> String(bool reverse)
	{
		const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
		var alphabet = Alphabet.Select(c => new string(c, 5)).ToArray();

		return new(reverse)
		{
			Enumerable.Empty<string>(),
			new[] { "", "", "", "", "" },
			{ alphabet, reverse ? new[] { "aaaaa" } : alphabet },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<int> Composite(bool reverse)
	{
		return new(reverse)
		{
			Enumerable.Empty<int>(),
			{
				Enumerable.Range(0, 128),
				reverse
					? Enumerable.Range(0, 128).Where(x => x % 2 == 0).Reverse()
					: Enumerable.Range(0, 128).Where((x, i) => i == 0 || x % 2 == 1)
			},
		};
	}
}
