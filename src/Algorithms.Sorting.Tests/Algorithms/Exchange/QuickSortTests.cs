using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="QuickSort{TKey}"/>.
/// </summary>
public sealed class QuickSortTests : SortingAlgorithmTestBase<QuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(QuickSort<>);
}
