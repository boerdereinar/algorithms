using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="BubbleSort{TKey}"/>.
/// </summary>
public sealed class BubbleSortTests : SortingAlgorithmTestBase<BubbleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BubbleSort<>);
}
