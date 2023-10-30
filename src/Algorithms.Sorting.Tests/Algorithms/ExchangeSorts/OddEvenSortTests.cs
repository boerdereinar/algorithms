using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="OddEvenSort{TKey}"/>.
/// </summary>
public sealed class OddEvenSortTests : SortingAlgorithmTestBase<OddEvenSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(OddEvenSort<>);
}
