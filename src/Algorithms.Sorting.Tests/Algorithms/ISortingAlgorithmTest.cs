using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms;

/// <summary>
/// Sorting algorithm test.
/// </summary>
public interface ISortingAlgorithmTest
{
	/// <summary>
	/// Gets the data for sorting integers.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <returns>The data for sorting integers.</returns>
	static virtual SortTheoryData<int> Integer(bool reverse)
	{
		return SortTheoryDataExtensions.CreateNumeric<int>(reverse);
	}

	/// <summary>
	/// Gets the data for sorting unsigned integers.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <returns>The data for sorting unsigned integers.</returns>
	static virtual SortTheoryData<uint> UnsignedInteger(bool reverse)
	{
		return SortTheoryDataExtensions.CreateNumeric<uint>(reverse);
	}

	/// <summary>
	/// Gets the data for sorting doubles.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <returns>The data for sorting doubles.</returns>
	static virtual SortTheoryData<double> Double(bool reverse)
	{
		return SortTheoryDataExtensions.CreateNumeric<double>(reverse);
	}

	/// <summary>
	/// Gets the data for sorting strings.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <returns>The data for sorting strings.</returns>
	static virtual SortTheoryData<string> String(bool reverse)
	{
		const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

		return new(reverse)
		{
			Enumerable.Empty<string>(),
			Enumerable.Range(0, 64).Select(_ => ""),
			{ Alphabet.Select(c => new string(c, 5)), true },
			{ Alphabet.SelectMany(c => Enumerable.Range(1, 5).Select(n => new string(c, n))), true },
			{ Alphabet[..10].SelectMany(a => Alphabet.SelectMany(b => new[] { $"{a}{b}a", $"{a}{b}b", $"{a}{b}c" })), true },
		};
	}

	/// <summary>
	/// Gets the data for sorting composite data. Composite sort tests assume that the sorted data is first ordered by
	/// parity then by value.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <returns>The data for sorting composite data.</returns>
	static virtual SortTheoryData<int> Composite(bool reverse)
	{
		var sorted = Enumerable.Range(0, 128).OrderBy(x => x % 2).ThenBy(x => x).ToArray();
		return new(reverse)
		{
			Enumerable.Empty<int>(),
			{ sorted, sorted },
			{ sorted, true },
		};
	}
}
