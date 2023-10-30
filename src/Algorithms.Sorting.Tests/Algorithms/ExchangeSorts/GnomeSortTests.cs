using Algorithms.Sorting.Algorithms.ExchangeSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="GnomeSort{TKey}"/>.
/// </summary>
public sealed class GnomeSortTests : SortingAlgorithmTestBase<GnomeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(GnomeSort<>);
}
