using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="PatienceSort{TKey}"/>.
/// </summary>
public sealed class PatienceSortTests : SortingAlgorithmTestBase<PatienceSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(PatienceSort<>);
}
