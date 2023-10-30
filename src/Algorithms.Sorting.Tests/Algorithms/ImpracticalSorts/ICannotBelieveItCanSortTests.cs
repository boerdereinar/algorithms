using Algorithms.Sorting.Algorithms.ImpracticalSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="ICannotBelieveItCanSort{TKey}"/>.
/// </summary>
public sealed class ICannotBelieveItCanSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(ICannotBelieveItCanSort<int>);
}
