using SortingAlgorithms.Algorithms.ImpracticalSorts;

namespace SortingAlgorithms.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="SlowSort{TKey}"/>.
/// </summary>
public sealed class SlowSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(SlowSort<>);
}
