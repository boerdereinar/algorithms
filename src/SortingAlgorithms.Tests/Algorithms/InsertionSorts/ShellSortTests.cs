using SortingAlgorithms.Algorithms.InsertionSorts;

namespace SortingAlgorithms.Tests.Algorithms.InsertionSorts;

/// <summary>
/// Tests for <see cref="ShellSort{TKey}"/>.
/// </summary>
public sealed class ShellSortTests : SortingAlgorithmTestBase<ShellSortTests>, ISortingAlgorithmTest
{
	/// <summary>
	/// Gets the gap sequences.
	/// </summary>
	public static TheoryData<ShellSortGapSequence, int, IEnumerable<int>> GapSequences => new()
	{
		{ ShellSortGapSequence.Shell,             32,  new[] { 16, 8, 4, 2, 1 } },
		{ ShellSortGapSequence.FrankLazarus,      32,  new[] { 17, 9, 5, 3, 1 } },
		{ ShellSortGapSequence.Hibbard,           63,  new[] { 31, 15, 7, 3, 1 } },
		{ ShellSortGapSequence.PapernovStasevich, 65,  new[] { 33, 17, 9, 5, 3, 1 } },
		{ ShellSortGapSequence.Pratt,             12,  new[] { 9, 8, 6, 4, 3, 2, 1 } },
		{ ShellSortGapSequence.Knuth,             363, new[] { 121, 40, 13, 4, 1 } },
		{ ShellSortGapSequence.IncerpiSedgewick,  336, new[] { 112, 48, 21, 7, 3, 1 } },
		{ ShellSortGapSequence.Sedgewick1982,     281, new[] { 77, 23, 8, 1 } },
		{ ShellSortGapSequence.Sedgewick1986,     109, new[] { 41, 19, 5, 1 } },
		{ ShellSortGapSequence.GonnetBaezaYates,  100, new[] { 45, 20, 9, 4, 1 } },
		{ ShellSortGapSequence.Tokuda,            103, new[] { 46, 20, 9, 4, 1 } },
		{ ShellSortGapSequence.Ciura,             701, new[] { 301, 132, 57, 23, 10, 4, 1 } },
		{ ShellSortGapSequence.Lee,               103, new[] { 102, 45, 20, 9, 4, 1 } },
	};

	/// <inheritdoc />
	protected override Type Type => typeof(ShellSort<>);

	/// <summary>
	/// Tests if <see cref="ShellSortGapSequence"/> returns the correct gap sequence.
	/// </summary>
	/// <param name="gapSequence">Gap sequence.</param>
	/// <param name="n">Max gap.</param>
	/// <param name="expected">Expected gap sequence.</param>
	[Theory]
	[MemberData(nameof(GapSequences))]
	public void ShellSortGapSequence_ReturnsCorrectGapSequence(ShellSortGapSequence gapSequence, int n, IEnumerable<int> expected)
	{
		Assert.Equal(expected, gapSequence.GapSequence(n));
	}
}
