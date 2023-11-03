using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="RedBlackTreeSort{TKey}"/>.
/// </summary>
public sealed class RedBlackTreeSortTests : SortingAlgorithmTestBase<RedBlackTreeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(RedBlackTreeSort<>);
}
