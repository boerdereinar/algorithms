using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="BubbleSort{TKey}"/>.
/// </summary>
public sealed class BubbleSortTests : SortingAlgorithmTestBase<BubbleSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BubbleSort<>);
}
