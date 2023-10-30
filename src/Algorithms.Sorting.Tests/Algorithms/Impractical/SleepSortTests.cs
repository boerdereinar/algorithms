using Algorithms.Sorting.Algorithms.Impractical;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="SleepSort{TKey}"/>.
/// </summary>
public sealed class SleepSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(SleepSort<>);
}
