using Algorithms.Sorting.Algorithms.Impractical;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="ICannotBelieveItCanSort{TKey}"/>.
/// </summary>
public sealed class ICannotBelieveItCanSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(ICannotBelieveItCanSort<int>);
}
