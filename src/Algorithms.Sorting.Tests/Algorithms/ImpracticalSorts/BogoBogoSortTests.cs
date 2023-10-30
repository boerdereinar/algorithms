using Algorithms.Sorting.Algorithms.ImpracticalSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="BogoBogoSort{TKey}"/>.
/// </summary>
public sealed class BogoBogoSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(BogoBogoSort<>);
}
