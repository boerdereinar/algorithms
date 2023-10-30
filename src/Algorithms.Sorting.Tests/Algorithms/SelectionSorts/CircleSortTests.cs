using Algorithms.Sorting.Algorithms.SelectionSorts;

namespace Algorithms.Sorting.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="CircleSort{TKey}"/>.
/// </summary>
public sealed class CircleSortTests : SortingAlgorithmTestBase<CircleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CircleSort<>);
}
