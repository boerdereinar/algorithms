using System.Numerics;
using SortingAlgorithms.Algorithms.ImpracticalSorts;
using SortingAlgorithms.Tests.TestUtilities;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="AssumptionSort{TKey}"/>.
/// </summary>
public sealed class AssumptionSortTests : SortingAlgorithmTestBase<AssumptionSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(AssumptionSort<>);

	/// <inheritdoc />
	public static SortTheoryData<int> Integer(bool reverse)
	{
		return AssumptionData<int>();
	}

	/// <inheritdoc />
	public static SortTheoryData<uint> UnsignedInteger(bool reverse)
	{
		return AssumptionData<uint>();
	}

	/// <inheritdoc />
	public static SortTheoryData<double> Double(bool reverse)
	{
		return AssumptionData<double>();
	}

	/// <inheritdoc />
	public static SortTheoryData<string> String(bool reverse)
	{
		const string Alphabet = "abcdefghijklmnopqrstuvwxyz";
		var alphabet = Alphabet.Select(c => new string(c, 5)).ToArray();

		return new()
		{
			Enumerable.Empty<string>(),
			new[] { "", "", "", "", "" },
			{ alphabet, alphabet },
		};
	}

	/// <inheritdoc />
	public static SortTheoryData<int> Composite(bool reverse)
	{
		return AssumptionData<int>();
	}

	private static SortTheoryData<T> AssumptionData<T>() where T : INumber<T>
	{
		var random = new Random(42);
		var shuffled = random.Shuffle(Enumerable.Range(0, 128)).ToArray();

		return new()
		{
			Enumerable.Empty<T>(),
			{ Enumerable.Range(0, 128), Enumerable.Range(0, 128) },
			{ shuffled, shuffled },
		};
	}
}
