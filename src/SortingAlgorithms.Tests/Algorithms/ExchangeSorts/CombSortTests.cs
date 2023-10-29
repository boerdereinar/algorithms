using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="CombSort{TKey}"/>.
/// </summary>
public sealed class CombSortTests : SortingAlgorithmTestBase<CombSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CombSort<>);
}
