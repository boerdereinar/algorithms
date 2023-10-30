using Algorithms.Sorting.Algorithms.Distribution;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.Distribution;

/// <summary>
/// Tests for <see cref="CountingSort{TKey}"/>.
/// </summary>
public sealed class CountingSortTests : SortingAlgorithmTestBase<CountingSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CountingSort<>);

	/// <inheritdoc />
	public static SortTheoryData<int> Integer(bool reverse)
	{
		return SortTheoryDataExtensions.CreateNumeric<int>(reverse, false);
	}

	/// <inheritdoc />
	public static SortTheoryData<uint> UnsignedInteger(bool reverse)
	{
		return SortTheoryDataExtensions.CreateNumeric<uint>(reverse, false);
	}
}
