using Algorithms.Sorting.Algorithms.Impractical;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="BozoSort{TKey}"/>.
/// </summary>
public sealed class BozoSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(BozoSort<>);
}
