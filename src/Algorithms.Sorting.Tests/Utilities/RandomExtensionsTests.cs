using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.Tests.Utilities;

/// <summary>
/// Tests for <see cref="RandomExtensions"/>.
/// </summary>
public sealed class RandomExtensionsTests
{
	/// <summary>
	/// Tests if <see cref="RandomExtensions.Shuffle{TSource}(Random,IEnumerable{TSource})"/> returns a shuffled sequence.
	/// </summary>
	[Fact]
	public void ShuffleIEnumerable_ReturnsShuffled()
	{
		var random = new Random(42);
		var actual = random.Shuffle(Enumerable.Range(0, 100)).ToArray();

		Assert.Equal(100, actual.Length);
		Assert.NotEqual(Enumerable.Range(0, 100), actual);
	}

	/// <summary>
	/// Tests if <see cref="RandomExtensions.Shuffle{TElement,TKey}(Random,KeyedArray{TElement,TKey})"/> returns a shuffled sequence.
	/// </summary>
	[Fact]
	public void ShuffleKeyedArray_ReturnsShuffled()
	{
		var random = new Random(42);
		var actual = Enumerable.Range(0, 100).ToKeyedArray(x => x, Comparer<int>.Default);
		random.Shuffle(actual);

		Assert.NotEqual(Enumerable.Range(0, 100), actual);
		Assert.Equal(actual, actual.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="RandomExtensions.Shuffle{TElement,TKey}(Random,KeyedArraySegment{TElement,TKey})"/> returns a shuffled sequence.
	/// </summary>
	[Fact]
	public void ShuffleKeyedArraySegment_ReturnsShuffled()
	{
		var random = new Random(42);
		var actual = new KeyedArraySegment<int, int>(Enumerable.Range(0, 100).ToKeyedArray(x => x, Comparer<int>.Default));
		random.Shuffle(actual);

		Assert.NotEqual(Enumerable.Range(0, 100), actual);
		Assert.Equal(actual.Elements.ToArray(), actual.Keys.ToArray());
	}
}