using Algorithms.Sorting.Algorithms.ImpracticalSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="StoogeSort{TKey}"/>.
/// </summary>
public sealed class StoogeSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(StoogeSort<>);
}
