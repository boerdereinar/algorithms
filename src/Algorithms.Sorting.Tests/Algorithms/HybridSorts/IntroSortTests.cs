using Algorithms.Sorting.Algorithms.HybridSorts;

namespace Algorithms.Sorting.Tests.Algorithms.HybridSorts;

/// <summary>
/// Tests for <see cref="IntroSort{TKey}"/>.
/// </summary>
public sealed class IntroSortTests : SortingAlgorithmTestBase<IntroSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(IntroSort<>);
}
