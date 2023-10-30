using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="ExchangeSort{TKey}"/>.
/// </summary>
public sealed class ExchangeSortTests : SortingAlgorithmTestBase<ExchangeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(ExchangeSort<>);
}
