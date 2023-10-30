using Algorithms.Sorting.Algorithms.InsertionSorts;

namespace Algorithms.Sorting.Tests.Algorithms.InsertionSorts;

/// <summary>
/// Tests for <see cref="InsertionSort{TKey}"/>.
/// </summary>
public sealed class InsertionSortTests : SortingAlgorithmTestBase<InsertionSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(InsertionSort<>);
}
