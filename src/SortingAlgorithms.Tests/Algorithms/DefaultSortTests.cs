using SortingAlgorithms.Algorithms;

namespace SortingAlgorithms.Tests.Algorithms;

/// <summary>
/// Tests for <see cref="DefaultSort{TKey}"/>.
/// </summary>
public sealed class DefaultSortTests : SortingAlgorithmTestBase<DefaultSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(DefaultSort<>);
}
