using SortingAlgorithms.Algorithms.SelectionSorts;

namespace SortingAlgorithms.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="CircleSort{TKey}"/>.
/// </summary>
public sealed class CircleSortTests : SortingAlgorithmTestBase<CircleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CircleSort<>);
}
