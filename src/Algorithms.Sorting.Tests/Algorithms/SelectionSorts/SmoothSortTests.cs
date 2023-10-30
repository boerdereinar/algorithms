using Algorithms.Sorting.Algorithms.SelectionSorts;

namespace Algorithms.Sorting.Tests.Algorithms.SelectionSorts;

/// <summary>
/// Tests for <see cref="SmoothSort{TKey}"/>.
/// </summary>
public sealed class SmoothSortTests : SortingAlgorithmTestBase<SmoothSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(SmoothSort<>);
}
