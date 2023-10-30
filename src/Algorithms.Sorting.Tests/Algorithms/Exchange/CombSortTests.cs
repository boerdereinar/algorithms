using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="CombSort{TKey}"/>.
/// </summary>
public sealed class CombSortTests : SortingAlgorithmTestBase<CombSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CombSort<>);
}
