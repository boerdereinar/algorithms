using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="QuickSort{TKey}"/>.
/// </summary>
public sealed class QuickSortTests : SortingAlgorithmTestBase<QuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(QuickSort<>);
}
