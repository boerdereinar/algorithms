using System.Numerics;
using Algorithms.Sorting.Algorithms.Impractical;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

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
		var sorted = Enumerable.Range(0, 128).ToArray();
		var shuffled = sorted.ToArray();
		random.Shuffle(shuffled);

		return new()
		{
			Enumerable.Empty<T>(),
			{ sorted, sorted },
			{ shuffled, shuffled },
		};
	}
}
