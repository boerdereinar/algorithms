using Algorithms.Sorting.Algorithms.Selection;

namespace Algorithms.Sorting.Tests.Algorithms.Selection;

/// <summary>
/// Tests for <see cref="SmoothSort{TKey}"/>.
/// </summary>
public sealed class SmoothSortTests : SortingAlgorithmTestBase<SmoothSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(SmoothSort<>);
}
