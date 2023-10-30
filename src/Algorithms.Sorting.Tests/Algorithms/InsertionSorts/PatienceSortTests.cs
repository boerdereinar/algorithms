using Algorithms.Sorting.Algorithms.InsertionSorts;

namespace Algorithms.Sorting.Tests.Algorithms.InsertionSorts;

/// <summary>
/// Tests for <see cref="PatienceSort{TKey}"/>.
/// </summary>
public sealed class PatienceSortTests : SortingAlgorithmTestBase<PatienceSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PatienceSort<>);
}
