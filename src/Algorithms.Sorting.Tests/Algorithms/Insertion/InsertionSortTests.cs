using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="InsertionSort{TKey}"/>.
/// </summary>
public sealed class InsertionSortTests : SortingAlgorithmTestBase<InsertionSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(InsertionSort<>);
}
