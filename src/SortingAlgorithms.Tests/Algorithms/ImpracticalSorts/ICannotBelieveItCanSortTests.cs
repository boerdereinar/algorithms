using SortingAlgorithms.Algorithms.ImpracticalSorts;

namespace SortingAlgorithms.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="ICannotBelieveItCanSort{TKey}"/>.
/// </summary>
public sealed class ICannotBelieveItCanSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(ICannotBelieveItCanSort<int>);
}
