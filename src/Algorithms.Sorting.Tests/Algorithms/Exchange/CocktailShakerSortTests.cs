using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="CocktailShakerSort{TKey}"/>.
/// </summary>
public sealed class CocktailShakerSortTests : SortingAlgorithmTestBase<CocktailShakerSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CocktailShakerSort<>);
}
