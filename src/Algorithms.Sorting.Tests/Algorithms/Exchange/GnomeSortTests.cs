using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="GnomeSort{TKey}"/>.
/// </summary>
public sealed class GnomeSortTests : SortingAlgorithmTestBase<GnomeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(GnomeSort<>);
}
