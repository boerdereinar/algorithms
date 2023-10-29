using SortingAlgorithms.Algorithms.ExchangeSorts;

namespace SortingAlgorithms.Tests.Algorithms.ExchangeSorts;

/// <summary>
/// Tests for <see cref="GnomeSort{TKey}"/>.
/// </summary>
public sealed class GnomeSortTests : SortingAlgorithmTestBase<GnomeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(GnomeSort<>);
}
