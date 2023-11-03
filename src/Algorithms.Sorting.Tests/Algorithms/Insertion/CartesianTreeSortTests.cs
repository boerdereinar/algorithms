using Algorithms.Sorting.Algorithms.Insertion;

namespace Algorithms.Sorting.Tests.Algorithms.Insertion;

/// <summary>
/// Tests for <see cref="CartesianTreeSort{TKey}"/>.
/// </summary>
public sealed class CartesianTreeSortTests : SortingAlgorithmTestBase<CartesianTreeSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(CartesianTreeSort<>);
}
