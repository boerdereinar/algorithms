using SortingAlgorithms.Algorithms.DistributionSorts;
using SortingAlgorithms.Tests.TestUtilities;

namespace SortingAlgorithms.Tests.Algorithms.DistributionSorts;

/// <summary>
/// Tests for <see cref="PigeonholeSort{TKey}"/>.
/// </summary>
public sealed class PigeonholeSortTests : SortingAlgorithmTestBase<PigeonholeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PigeonholeSort<>);

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
