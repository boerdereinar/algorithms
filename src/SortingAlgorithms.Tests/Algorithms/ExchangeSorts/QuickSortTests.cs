using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="QuickSort{TKey}"/>.
/// </summary>
public sealed class QuickSortTests : SortingAlgorithmTestBase<QuickSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(QuickSort<>);
}
