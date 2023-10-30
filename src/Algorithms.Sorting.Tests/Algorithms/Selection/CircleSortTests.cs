using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="CircleSort{TKey}"/>.
/// </summary>
public sealed class CircleSortTests : SortingAlgorithmTestBase<CircleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CircleSort<>);
}
