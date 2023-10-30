using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="TournamentSort{TKey}"/>.
/// </summary>
public sealed class TournamentSortTests : SortingAlgorithmTestBase<TournamentSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(TournamentSort<>);
}
