using Algorithms.Sorting.Algorithms.Hybrid;

namespace Algorithms.Sorting.Tests.Algorithms.Hybrid;

/// <summary>
/// Tests for <see cref="IntroSort{TKey}"/>.
/// </summary>
public sealed class IntroSortTests : SortingAlgorithmTestBase<IntroSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(IntroSort<>);
}
