using SortingAlgorithms.Algorithms;
using SortingAlgorithms.Sorting;
using SortingAlgorithms.Utilities;

namespace SortingAlgorithms.Tests.Sorting;

/// <summary>
/// Tests for <see cref="OrderedEnumerable{TSource,TCompositeKey}"/>.
/// </summary>
public sealed class OrderedEnumerableTests
{
	/// <summary>
	/// Tests if <see cref="OrderedEnumerable{TSource,TCompositeKey}"/> sorts correctly.
	/// </summary>
	[Fact]
	public void OrderedEnumerable_SortsCorrectly()
	{
		var random = new Random(42);
		var source = random.Shuffle(Enumerable.Range(0, 100)).ToArray();
		var expected = Enumerable.Range(0, 100).ToArray();

		var actual = new OrderedEnumerable<int, int>(source, x => x, Comparer<int>.Default, new DefaultSort<int>());

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="OrderedEnumerable{TSource,TCompositeKey}.CreateOrderedEnumerable{TKey}"/> sorts correctly.
	/// </summary>
	/// <param name="descending">Whether the data is reversed.</param>
	[Theory]
	[InlineData(false)]
	[InlineData(true)]
	public void CreateOrderedEnumerable_SortsCorrectly(bool descending)
	{
		var random = new Random(42);
		var source = random.Shuffle(Enumerable.Range(0, 128)).ToArray();
		var expected = Enumerable.Range(0, 128).OrderBy(x => x % 2);
		expected = descending ? expected.ThenByDescending(x => x) : expected.ThenBy(x => x);

		var ordered = new OrderedEnumerable<int, int>(source, x => x % 2, Comparer<int>.Default, new DefaultSort<int>());
		var actual = ordered.CreateOrderedEnumerable(x => x, Comparer<int>.Default, descending);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="OrderedEnumerable{TSource,TCompositeKey}.CreateOrderedEnumerable{TKey}"/> sorts correctly when the comparer is null.
	/// </summary>
	[Fact]
	public void CreateOrderedEnumerable_NullComparer_SortsCorrectly()
	{
		var random = new Random(42);
		var source = random.Shuffle(Enumerable.Range(0, 100)).ToArray();
		var expected = Enumerable.Range(0, 100).OrderBy(x => x % 2).ThenBy(x => x).ToArray();

		var ordered = new OrderedEnumerable<int, int>(source, x => x % 2, Comparer<int>.Default, new DefaultSort<int>());
		var actual = ordered.CreateOrderedEnumerable(x => x, Comparer<int>.Default, false);

		Assert.Equal(expected, actual);
	}
}
