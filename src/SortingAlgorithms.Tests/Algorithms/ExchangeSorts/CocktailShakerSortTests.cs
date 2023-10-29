using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="CocktailShakerSort{TKey}"/>.
/// </summary>
public sealed class CocktailShakerSortTests : SortingAlgorithmTestBase<CocktailShakerSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CocktailShakerSort<>);
}
