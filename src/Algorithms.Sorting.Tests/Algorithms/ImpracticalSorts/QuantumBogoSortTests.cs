using Algorithms.Sorting.Algorithms.ImpracticalSorts;

namespace Algorithms.Sorting.Tests.Algorithms.ImpracticalSorts;

/// <summary>
/// Tests for <see cref="QuantumBogoSort{TKey}"/>.
/// </summary>
public sealed class QuantumBogoSortTests : SortingAlgorithmTestBase<QuantumBogoSortTests>, ISortingAlgorithmTest
{
	/// <inheritdoc />
	protected override Type Type => typeof(QuantumBogoSort<>);

	/// <inheritdoc />
	public override void SortComposite_SortsCorrectly(IEnumerable<int> source, IEnumerable<int> expected, bool reverse)
	{
		var sourceArray = source.ToArray();
		var expectedArray = expected.ToArray();

		if (sourceArray.SequenceEqual(expectedArray))
			base.SortComposite_SortsCorrectly(sourceArray, expectedArray, reverse);
		else
			Assert.Throws<EndOfTheUniverseException>(() =>
				base.SortComposite_SortsCorrectly(sourceArray, expectedArray, reverse));
	}

	/// <inheritdoc />
	protected override void SortTestBase<TKey>(IEnumerable<TKey> source, IEnumerable<TKey> expected, bool reverse)
	{
		var sourceArray = source.ToArray();
		var expectedArray = expected.ToArray();

		if (sourceArray.SequenceEqual(expectedArray))
			base.SortTestBase(sourceArray, expectedArray, reverse);
		else
			Assert.Throws<EndOfTheUniverseException>(() =>
				base.SortTestBase(sourceArray, expectedArray, reverse));
	}
}
