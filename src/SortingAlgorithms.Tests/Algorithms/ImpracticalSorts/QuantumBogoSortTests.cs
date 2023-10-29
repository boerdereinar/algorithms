using SortingAlgorithms.Algorithms.ImpracticalSorts;

namespace SortingAlgorithms.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="QuantumBogoSort{TKey}"/>.
/// </summary>
public sealed class QuantumBogoSortTests : SortingAlgorithmTestBase<QuantumBogoSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(QuantumBogoSort<>);

	/// <inheritdoc />
	public override async Task SortComposite_SortsCorrectly(IEnumerable<int> source, IEnumerable<int> expected, bool reverse)
	{
		var sourceArray = source.ToArray();
		var expectedArray = expected.ToArray();

		if (sourceArray.SequenceEqual(expectedArray))
			await base.SortComposite_SortsCorrectly(sourceArray, expectedArray, reverse);
		else
			await Assert.ThrowsAsync<EndOfTheUniverseException>(() =>
				base.SortComposite_SortsCorrectly(sourceArray, expectedArray, reverse));
	}

	/// <inheritdoc />
	protected override async Task SortTestBase<TKey>(IEnumerable<TKey> source, IEnumerable<TKey> expected, bool reverse)
	{
		var sourceArray = source.ToArray();
		var expectedArray = expected.ToArray();

		if (sourceArray.SequenceEqual(expectedArray))
			await base.SortTestBase(sourceArray, expectedArray, reverse);
		else
			await Assert.ThrowsAsync<EndOfTheUniverseException>(() =>
				base.SortTestBase(sourceArray, expectedArray, reverse));
	}
}
