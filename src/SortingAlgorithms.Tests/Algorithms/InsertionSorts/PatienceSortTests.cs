using SortingAlgorithms.Algorithms.InsertionSorts;

namespace SortingAlgorithms.Tests.Algorithms.InsertionSorts;

/// <summary>
/// Tests for <see cref="PatienceSort{TKey}"/>.
/// </summary>
public sealed class PatienceSortTests : SortingAlgorithmTestBase<PatienceSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PatienceSort<>);
}
