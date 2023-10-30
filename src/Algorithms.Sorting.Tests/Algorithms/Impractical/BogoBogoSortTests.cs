using Algorithms.Sorting.Algorithms.Impractical;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="BogoBogoSort{TKey}"/>.
/// </summary>
public sealed class BogoBogoSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(BogoBogoSort<>);
}
