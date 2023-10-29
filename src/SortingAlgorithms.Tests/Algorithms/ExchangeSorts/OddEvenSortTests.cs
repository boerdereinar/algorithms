using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="OddEvenSort{TKey}"/>.
/// </summary>
public sealed class OddEvenSortTests : SortingAlgorithmTestBase<OddEvenSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(OddEvenSort<>);
}
