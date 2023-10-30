using Algorithms.Sorting.Algorithms.Impractical;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="StoogeSort{TKey}"/>.
/// </summary>
public sealed class StoogeSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(StoogeSort<>);
}
