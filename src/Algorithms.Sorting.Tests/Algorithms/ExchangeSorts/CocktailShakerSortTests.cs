using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="CocktailShakerSort{TKey}"/>.
/// </summary>
public sealed class CocktailShakerSortTests : SortingAlgorithmTestBase<CocktailShakerSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CocktailShakerSort<>);
}
