using Algorithms.Common.Collections;
using Algorithms.Common.Utilities;

namespace Algorithms.Common.Tests.Utilities;

/// <summary>
/// Tests for <see cref="RandomExtensions"/>.
/// </summary>
public sealed class RandomExtensionsTests
{
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
		Assert.Equal(actual, actual.Keys);
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
		Assert.Equal(actual.Elements, actual.Keys);
	}
}
