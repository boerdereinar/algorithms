using SortingAlgorithms.Algorithms.ImpracticalSorts;

namespace SortingAlgorithms.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="SleepSort{TKey}"/>.
/// </summary>
public sealed class SleepSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(SleepSort<>);
}
