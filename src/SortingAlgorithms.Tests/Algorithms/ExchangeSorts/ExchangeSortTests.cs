using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="ExchangeSort{TKey}"/>.
/// </summary>
public sealed class ExchangeSortTests : SortingAlgorithmTestBase<ExchangeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(ExchangeSort<>);
}
