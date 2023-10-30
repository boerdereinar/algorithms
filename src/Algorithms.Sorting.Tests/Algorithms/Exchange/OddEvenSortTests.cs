using Algorithms.Sorting.Algorithms.Exchange;

namespace Algorithms.Sorting.Tests.Algorithms.Exchange;

/// <summary>
/// Tests for <see cref="OddEvenSort{TKey}"/>.
/// </summary>
public sealed class OddEvenSortTests : SortingAlgorithmTestBase<OddEvenSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(OddEvenSort<>);
}
