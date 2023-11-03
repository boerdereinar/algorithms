using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="SplaySort{TKey}"/>.
/// </summary>
public sealed class SplaySortTests : SortingAlgorithmTestBase<SplaySortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(SplaySort<>);
}
