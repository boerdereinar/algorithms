using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="BinaryTreeSort{TKey}"/>.
/// </summary>
public sealed class BinaryTreeSortTests : SortingAlgorithmTestBase<BinaryTreeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(BinaryTreeSort<>);
}
