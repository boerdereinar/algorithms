using Algorithms.Sorting.Algorithms.ImpracticalSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="BozoSort{TKey}"/>.
/// </summary>
public sealed class BozoSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(BozoSort<>);
}
