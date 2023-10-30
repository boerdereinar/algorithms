using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="CombSort{TKey}"/>.
/// </summary>
public sealed class CombSortTests : SortingAlgorithmTestBase<CombSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CombSort<>);
}
