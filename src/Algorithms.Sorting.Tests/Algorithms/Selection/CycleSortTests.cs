using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="CycleSort{TKey}"/>.
/// </summary>
public sealed class CycleSortTests : SortingAlgorithmTestBase<CycleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CycleSort<>);
}
