using SortingAlgorithms.Sorting;
using SortingAlgorithms.Tests.TestUtilities;

namespace SortingAlgorithms.Tests.Sorting;

/// <summary>
/// Tests for <see cref="KeyedArray{TElement,TKey}"/>.
/// </summary>
public sealed class KeyedArrayTests
{
	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> constructor works as expected with a key selector.
	/// </summary>
	[Fact]
	public void Constructor_KeySelector_CreatesKeyedArrayWithKeys()
	{
		var source = new[] { 1, 2, 3, 4, 5 };
		var keys = source.Select(x => x * 2).ToArray();
		var comparer = Comparer<int>.Default;
		var actual = new KeyedArray<int, int>(source, x => x * 2, comparer);

		Assert.Equal(source, actual.Elements);
		Assert.Equal(keys, actual.Keys);
		Assert.Equal(comparer, actual.Comparer);
		Assert.Equal(source.Length, actual.Length);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> constructor throws an <see cref="ArgumentOutOfRangeException"/> if the size is less than zero.
	/// </summary>
	[Fact]
	public void Constructor_SizeLessThanZero_ThrowsArgumentOutOfRangeException()
	{
		var ex = Assert.Throws<ArgumentOutOfRangeException>("size", () => new KeyedArray<int, int>(-1, Comparer<int>.Default));
		Assert.Equal(-1, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> constructor works as expected with a size.
	/// </summary>
	[Fact]
	public void Constructor_Size_CreatesKeyedArray()
	{
		var comparer = Comparer<int>.Default;
		var actual = new KeyedArray<int, int>(4, comparer);

		Assert.Equal(Enumerable.Repeat(0, 4), actual.Elements);
		Assert.Equal(Enumerable.Repeat(0, 4), actual.Keys);
		Assert.Equal(comparer, actual.Comparer);
		Assert.Equal(4, actual.Length);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> copy constructor works as expected.
	/// </summary>
	[Fact]
	public void CopyConstructor_CopiesKeyedArray()
	{
		var expected = KeyedArray();
		var actual = new KeyedArray<int, int>(expected);

		Assert.Equal(expected.Elements, actual.Elements);
		Assert.NotSame(expected.Elements, actual.Elements);
		Assert.Equal(expected.Keys, actual.Keys);
		Assert.NotSame(expected.Keys, actual.Keys);
		Assert.Equal(expected.Comparer, actual.Comparer);
		Assert.Equal(expected.Length, actual.Length);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> copy constructor throws an <see cref="ArgumentOutOfRangeException"/> if the offset is invalid.
	/// </summary>
	/// <param name="offset">The offset.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(6)]
	public void CopyConstructor_InvalidOffset_ThrowsArgumentOutOfRangeException(int offset)
	{
		var source = KeyedArray();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(offset), () => new KeyedArray<int, int>(source, offset, 0));
		Assert.Equal(offset, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> copy constructor throws an <see cref="ArgumentOutOfRangeException"/> if offset + count is invalid.
	/// </summary>
	/// <param name="offset">The offset.</param>
	/// <param name="count">The count.</param>
	[Theory]
	[InlineData(0, 6)]
	[InlineData(3, 5)]
	public void CopyConstructor_InvalidOffsetPlusCount_ThrowsArgumentOutOfRangeException(int offset, int count)
	{
		var source = KeyedArray();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(count), () => new KeyedArray<int, int>(source, offset, count));
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> copy constructor works as expected with a range.
	/// </summary>
	[Fact]
	public void CopyConstructor_Range_CopiesPartialKeyedArray()
	{
		var source = KeyedArray();
		var actual = new KeyedArray<int, int>(source, 1, 3);

		Assert.Equal(new[] { 2, 3, 4 }, actual.Elements);
		Assert.Equal(new[] { 2, 3, 4 }, actual.Keys);
		Assert.Equal(Comparer<int>.Default, actual.Comparer);
		Assert.Equal(3, actual.Length);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> indexer gets the value at the specified index.
	/// </summary>
	[Fact]
	public void Indexer_Get_ReturnsItemAndValueAtIndex()
	{
		var source = KeyedArray(3);
		Assert.Equal((1, 1), source[0]);
		Assert.Equal((2, 2), source[1]);
		Assert.Equal((3, 3), source[2]);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArray{TElement,TKey}"/> indexer sets the value at the specified index.
	/// </summary>
	[Fact]
	public void Indexer_Set_SetsItemAndValueAtIndex()
	{
		var source = KeyedArray();
		source[0] = (4, 5);

		Assert.Equal(4, source.Elements[0]);
		Assert.Equal(5, source.Keys[0]);
		Assert.Equal((2, 2), source[1]);
		Assert.Equal((3, 3), source[2]);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Element(int)"/> returns the element at the specified index.
	/// </summary>
	[Fact]
	public void Element_Int_ReturnsKeyAtIndex()
	{
		var source = KeyedArray();

		Assert.Equal(1, source.Element(0));
		Assert.Equal(5, source.Element(4));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Element(Index)"/> returns the element at the specified index.
	/// </summary>
	[Fact]
	public void Element_Index_ReturnsKeyAtIndex()
	{
		var source = KeyedArray();

		Assert.Equal(1, source.Element(Index.FromStart(0)));
		Assert.Equal(5, source.Element(^1));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Key(int)"/> returns the key at the specified index.
	/// </summary>
	[Fact]
	public void Key_Int_ReturnsKeyAtIndex()
	{
		var source = KeyedArray();

		Assert.Equal(1, source.Key(0));
		Assert.Equal(5, source.Key(4));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Key(Index)"/> returns the key at the specified index.
	/// </summary>
	[Fact]
	public void Key_Index_ReturnsKeyAtIndex()
	{
		var source = KeyedArray();

		Assert.Equal(1, source.Key(Index.FromStart(0)));
		Assert.Equal(5, source.Key(^1));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Compare(int,int)"/> returns the expected result.
	/// </summary>
	/// <param name="i">The first index.</param>
	/// <param name="j">The second index.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[InlineData(0, 0, 0)]
	[InlineData(0, 1, -1)]
	[InlineData(1, 0, 1)]
	public void Compare_Int_ReturnsExpected(int i, int j, int expected)
	{
		var source = KeyedArray();
		var actual = source.Compare(i, j);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Compare(int,int)"/> returns the expected result.
	/// </summary>
	/// <param name="i">The first index.</param>
	/// <param name="j">The second index.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[InlineData(0, 0, 0)]
	[InlineData(0, 1, -1)]
	[InlineData(1, 0, 1)]
	public void Compare_Index_ReturnsExpected(Index i, Index j, int expected)
	{
		var source = KeyedArray();
		var actual = source.Compare(i, j);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Compare(int,TKey)"/> returns the expected result.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <param name="key">The key.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[InlineData(0, 1, 0)]
	[InlineData(0, 2, -1)]
	[InlineData(0, 0, 1)]
	public void Compare_IntToKey_ReturnsExpected(int i, uint key, int expected)
	{
		var source = new KeyedArray<int, uint>(new[] { 1, 2, 3 }, x => (uint)x, Comparer<uint>.Default);
		var actual = source.Compare(i, key);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Compare(Index,TKey)"/> returns the expected result.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <param name="key">The key.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[InlineData(0, 1, 0)]
	[InlineData(0, 2, -1)]
	[InlineData(0, 0, 1)]
	public void Compare_IndexToKey_ReturnsExpected(Index i, uint key, int expected)
	{
		var source = new KeyedArray<int, uint>(new[] { 1, 2, 3 }, x => (uint)x, Comparer<uint>.Default);
		var actual = source.Compare(i, key);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Swap(int,int)"/> swaps the values at the specified indices.
	/// </summary>
	[Fact]
	public void Swap_Int_SwapsValues()
	{
		var source = KeyedArray();
		source.Swap(0, 1);

		Assert.Equal(new[] { 2, 1, 3, 4, 5 }, source.Elements);
		Assert.Equal(new[] { 2, 1, 3, 4, 5 }, source.Keys);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Swap(Index,Index)"/> swaps the values at the specified indices.
	/// </summary>
	[Fact]
	public void Swap_Index_SwapsValues()
	{
		var source = KeyedArray();
		source.Swap(0, ^1);

		Assert.Equal(new[] { 5, 2, 3, 4, 1 }, source.Elements);
		Assert.Equal(new[] { 5, 2, 3, 4, 1 }, source.Keys);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Slice(int,int)"/> returns a <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	[Fact]
	public void Slice_ReturnsKeyedArraySegment()
	{
		var source = KeyedArray();
		var actual = source.Slice(1, 3);

		Assert.Equal(3, actual.Length);
		Assert.Equal(new[] { 2, 3, 4 }, actual.Elements.ToArray());
		Assert.Equal(new[] { 2, 3, 4 }, actual.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Resize(int)"/> resizes the array.
	/// </summary>
	[Fact]
	public void Resize_ResizesArray()
	{
		var source = KeyedArray();
		source.Resize(10);

		Assert.Equal(10, source.Length);
		Assert.Equal(10, source.Elements.Length);
		Assert.Equal(10, source.Keys.Length);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.CopyTo"/> copies the segment.
	/// </summary>
	[Fact]
	public void CopyTo_CopiesSegment()
	{
		var source = KeyedArray();
		var destination = new KeyedArraySegment<int, int>(new(5, Comparer<int>.Default));

		source.CopyTo(destination);

		Assert.NotSame(source, destination.Array);
		Assert.Equal(source.Length, destination.Length);
		Assert.Equal(source.Elements, destination.Elements.ToArray());
		Assert.Equal(source.Keys, destination.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.Clone()"/> clones the array.
	/// </summary>
	[Fact]
	public void Clone_ClonesArray()
	{
		var expected = KeyedArray();
		var actual = expected.Clone();

		Assert.Equal(expected.Elements, actual.Elements);
		Assert.NotSame(expected.Elements, actual.Elements);
		Assert.Equal(expected.Keys, actual.Keys);
		Assert.NotSame(expected.Keys, actual.Keys);
		Assert.Equal(expected.Comparer, actual.Comparer);
		Assert.Equal(expected.Length, actual.Length);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArray{TElement,TKey}.GetEnumerator()"/> returns the elements of the array.
	/// </summary>
	[Fact]
	public void GetEnumerator_ReturnsElements()
	{
		var source = KeyedArray();
		Assert.Equal(source.Elements, source);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArrayEnumerableExtensions.ToKeyedArray{TElement,TKey}"/> returns a <see cref="KeyedArray{TElement, TKey}"/>.
	/// </summary>
	[Fact]
	public void ToKeyedArray_ReturnsKeyedArray()
	{
		var source = new[] { 1, 2, 3, 4, 5 };
		var actual = source.ToKeyedArray(x => x, Comparer<int>.Default);

		Assert.Equal(source, actual.Elements);
		Assert.Equal(source, actual.Keys);
		Assert.Equal(source.Length, actual.Length);
	}

	private static KeyedArray<int, int> KeyedArray(int n = 5)
	{
		return new(Enumerable.Range(1, n).ToArray(), x => x, Comparer<int>.Default);
	}
}
