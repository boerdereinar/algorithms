using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="BubbleSort{TKey}"/>.
/// </summary>
public sealed class BubbleSortTests : SortingAlgorithmTestBase<BubbleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BubbleSort<>);
}
