using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="ExchangeSort{TKey}"/>.
/// </summary>
public sealed class ExchangeSortTests : SortingAlgorithmTestBase<ExchangeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(ExchangeSort<>);
}
