using Algorithms.Sorting.Algorithms.Impractical;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="SlowSort{TKey}"/>.
/// </summary>
public sealed class SlowSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(SlowSort<>);
}
