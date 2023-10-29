using SortingAlgorithms.Algorithms.SelectionSorts;

namespace SortingAlgorithms.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="TournamentSort{TKey}"/>.
/// </summary>
public sealed class TournamentSortTests : SortingAlgorithmTestBase<TournamentSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(TournamentSort<>);
}
