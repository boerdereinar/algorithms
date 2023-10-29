using SortingAlgorithms.Sorting;
using SortingAlgorithms.Tests.TestUtilities;

namespace SortingAlgorithms.Tests.Sorting;

/// <summary>
/// Tests for <see cref="KeyedArraySegment{TElement,TKey}"/>.
/// </summary>
public sealed class KeyedArraySegmentTests
{
	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> constructor works.
	/// </summary>
	[Fact]
	public void DefaultConstructor_WrapsArray()
	{
		var array = KeyedArray();
		var segment = new KeyedArraySegment<int, int>(array);

		Assert.Same(array, segment.Array);
		Assert.Equal(array.Length, segment.Length);
		Assert.Equal(array.Elements, segment.Elements.ToArray());
		Assert.Equal(array.Keys, segment.Keys.ToArray());
		Assert.Equal(0, segment.Offset);
		Assert.Equal(array.Length, segment.Length);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> constructor throws an
	/// <see cref="ArgumentOutOfRangeException"/> if the offset is invalid.
	/// </summary>
	/// <param name="offset">The offset.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(6)]
	public void Constructor_InvalidOffset_ThrowsArgumentOutOfRangeException(int offset)
	{
		var array = KeyedArray();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(offset), () => new KeyedArraySegment<int, int>(array, offset, 0));
		Assert.Equal(offset, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> constructor throws an
	/// <see cref="ArgumentOutOfRangeException"/> if offset + count is invalid.
	/// </summary>
	/// <param name="offset">The offset.</param>
	/// <param name="count">The count.</param>
	[Theory]
	[InlineData(0, 6)]
	[InlineData(3, 5)]
	public void Constructor_InvalidOffsetPlusCount_ThrowsArgumentOutOfRangeException(int offset, int count)
	{
		var array = KeyedArray();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(count), () => new KeyedArraySegment<int, int>(array, offset, count));
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> constructor works as expected with a range.
	/// </summary>
	[Fact]
	public void Constructor_Range_CreatesKeyedArraySegment()
	{
		var array = KeyedArray();
		var segment = new KeyedArraySegment<int, int>(array, 1, 3);

		Assert.Same(array, segment.Array);
		Assert.Equal(new[] { 1, 2, 3 }, new[] { 1, 2, 3 });
		Assert.Equal(new[] { 2, 3, 4 }, segment.Elements.ToArray());
		Assert.Equal(new[] { 2, 3, 4 }, segment.Keys.ToArray());
		Assert.Equal(1, segment.Offset);
		Assert.Equal(3, segment.Length);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> indexer throws an
	/// <see cref="ArgumentOutOfRangeException"/> if the index is invalid.
	/// </summary>
	/// <param name="i">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Indexer_Get_InvalidIndex_ThrowsArgumentOutOfRangeException(int i)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment[i]);
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> indexer gets the value at the specified index.
	/// </summary>
	[Fact]
	public void Indexer_Get_ReturnsItemAndValueAtIndex()
	{
		var segment = KeyedArraySegment();
		Assert.Equal((2, 2), segment[0]);
		Assert.Equal((3, 3), segment[1]);
		Assert.Equal((4, 4), segment[2]);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> indexer throws an
	/// <see cref="ArgumentOutOfRangeException"/> if the index is invalid.
	/// </summary>
	/// <param name="i">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Indexer_set_InvalidIndex_ThrowsArgumentOutOfRangeException(int i)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment[i] = (0, 0));
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> indexer sets the value at the specified index.
	/// </summary>
	[Fact]
	public void Indexer_Set_SetsItemAndValueAtIndex()
	{
		var segment = KeyedArraySegment();
		segment[0] = (4, 5);

		Assert.Equal(4, segment.Elements[0]);
		Assert.Equal(5, segment.Keys[0]);
		Assert.Equal(4, segment.Array.Elements[1]);
		Assert.Equal(5, segment.Array.Keys[1]);
		Assert.Equal((3, 3), segment[1]);
		Assert.Equal((4, 4), segment[2]);
	}

	/// <summary>
	/// Tests if the <see cref="KeyedArraySegment{TElement,TKey}"/> implicit operator returns the
	/// <see cref="KeyedArray{TElement,TKey}"/> wrapped by the <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	[Fact]
	public void Implicit_KeyedArray_ReturnsKeyedArraySegment()
	{
		var array = KeyedArray();
		var segment = (KeyedArraySegment<int, int>)array;

		Assert.Same(array, segment.Array);
		Assert.Equal(0, segment.Offset);
		Assert.Equal(array.Length, segment.Length);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Element(int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if the index is invalid.
	/// </summary>
	/// <param name="i">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Element_InvalidIndex_ThrowsArgumentOutOfRangeException(int i)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment.Element(i));
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Element(int)"/> returns the element at the specified index.
	/// </summary>
	[Fact]
	public void Element_Int_ReturnsKeyAtIndex()
	{
		var segment = KeyedArraySegment();

		Assert.Equal(2, segment.Element(0));
		Assert.Equal(4, segment.Element(2));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Element(Index)"/> returns the element at the specified
	/// index.
	/// </summary>
	[Fact]
	public void Element_Index_ReturnsKeyAtIndex()
	{
		var segment = KeyedArraySegment();

		Assert.Equal(2, segment.Element(Index.FromStart(0)));
		Assert.Equal(4, segment.Element(^1));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Key(int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if the index is invalid.
	/// </summary>
	/// <param name="i">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Key_InvalidIndex_ThrowsArgumentOutOfRangeException(int i)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment.Key(i));
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Key(int)"/> returns the key at the specified index.
	/// </summary>
	[Fact]
	public void Key_Int_ReturnsKeyAtIndex()
	{
		var segment = KeyedArraySegment();

		Assert.Equal(2, segment.Key(0));
		Assert.Equal(4, segment.Key(2));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Key(Index)"/> returns the key at the specified index.
	/// </summary>
	[Fact]
	public void Key_Index_ReturnsKeyAtIndex()
	{
		var segment = KeyedArraySegment();

		Assert.Equal(2, segment.Key(Index.FromStart(0)));
		Assert.Equal(4, segment.Key(^1));
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(int,int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if i is invalid.
	/// </summary>
	/// <param name="i">The first index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Compare_InvalidI_ThrowsArgumentOutOfRangeException(int i)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment.Compare(i, 0));
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(int,int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if j is invalid.
	/// </summary>
	/// <param name="j">The second index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Compare_InvalidJ_ThrowsArgumentOutOfRangeException(int j)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(j), () => segment.Compare(0, j));
		Assert.Equal(j, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(int,int)"/> returns the expected result.
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
		var segment = KeyedArraySegment();
		var actual = segment.Compare(i, j);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(int,int)"/> returns the expected result.
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
		var segment = KeyedArraySegment();
		var actual = segment.Compare(i, j);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(int,TKey)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if i is invalid.
	/// </summary>
	/// <param name="i">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Compare_InvalidIndex_ThrowsArgumentOutOfRangeException(int i)
	{
		var array = new KeyedArray<int, uint>(new[] { 1, 2, 3, 4, 5 }, x => (uint)x, Comparer<uint>.Default);
		var segment = new KeyedArraySegment<int, uint>(array, 1, 3);
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment.Compare(i, 0u));
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(int,TKey)"/> returns the expected result.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <param name="key">The key.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[InlineData(0, 2, 0)]
	[InlineData(0, 3, -1)]
	[InlineData(0, 1, 1)]
	public void Compare_IntToKey_ReturnsExpected(int i, uint key, int expected)
	{
		var array = new KeyedArray<int, uint>(new[] { 1, 2, 3, 4, 5 }, x => (uint)x, Comparer<uint>.Default);
		var segment = new KeyedArraySegment<int, uint>(array, 1, 3);
		var actual = segment.Compare(i, key);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Compare(Index,TKey)"/> returns the expected result.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <param name="key">The key.</param>
	/// <param name="expected">The expected result.</param>
	[Theory]
	[InlineData(0, 2, 0)]
	[InlineData(0, 3, -1)]
	[InlineData(0, 1, 1)]
	public void Compare_IndexToKey_ReturnsExpected(Index i, uint key, int expected)
	{
		var array = new KeyedArray<int, uint>(new[] { 1, 2, 3, 4, 5 }, x => (uint)x, Comparer<uint>.Default);
		var segment = new KeyedArraySegment<int, uint>(array, 1, 3);
		var actual = segment.Compare(i, key);

		Assert.Equal(expected, actual);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Swap(int,int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if i is invalid.
	/// </summary>
	/// <param name="i">The first index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Swap_InvalidI_ThrowsArgumentOutOfRangeException(int i)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(i), () => segment.Swap(i, 0));
		Assert.Equal(i, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Swap(int,int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if j is invalid.
	/// </summary>
	/// <param name="j">The second index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(3)]
	public void Swap_InvalidJ_ThrowsArgumentOutOfRangeException(int j)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(j), () => segment.Swap(0, j));
		Assert.Equal(j, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Swap(int,int)"/> swaps the values at the specified indices.
	/// </summary>
	[Fact]
	public void Swap_Int_SwapsValues()
	{
		var segment = KeyedArraySegment();
		segment.Swap(0, 1);

		Assert.Equal(new[] { 3, 2, 4 }, segment.Elements.ToArray());
		Assert.Equal(new[] { 3, 2, 4 }, segment.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Swap(Index,Index)"/> swaps the values at the specified
	/// indices.
	/// </summary>
	[Fact]
	public void Swap_Index_SwapsValues()
	{
		var segment = KeyedArraySegment();
		segment.Swap(0, ^1);

		Assert.Equal(new[] { 4, 3, 2 }, segment.Elements.ToArray());
		Assert.Equal(new[] { 4, 3, 2 }, segment.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Slice(int,int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if offset is invalid.
	/// </summary>
	/// <param name="index">The index.</param>
	[Theory]
	[InlineData(-1)]
	[InlineData(4)]
	public void Swap_InvalidIndex_ThrowsArgumentOutOfRangeException(int index)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(index), () => segment.Slice(index, 0));
		Assert.Equal(index, ex.ActualValue);
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Slice(int,int)"/> throws an
	/// <see cref="ArgumentOutOfRangeException"/> if index + count is invalid.
	/// </summary>
	/// <param name="index">The index.</param>
	/// <param name="count">The count.</param>
	[Theory]
	[InlineData(0, 4)]
	[InlineData(2, 2)]
	public void Swap_InvalidCount_ThrowsArgumentOutOfRangeException(int index, int count)
	{
		var segment = KeyedArraySegment();
		var ex = Assert.Throws<ArgumentOutOfRangeException>(nameof(count), () => segment.Slice(index, count));
		AssertException.MessageNotEmpty(ex);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Slice(int,int)"/> returns a
	/// <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	[Fact]
	public void Slice_ReturnsKeyedArraySegment()
	{
		var segment = KeyedArraySegment(7);
		var actual = segment.Slice(1, 3);

		Assert.Equal(3, actual.Length);
		Assert.Equal(new[] { 3, 4, 5 }, actual.Elements.ToArray());
		Assert.Equal(new[] { 3, 4, 5 }, actual.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.CopyTo"/> copies the segment.
	/// </summary>
	[Fact]
	public void CopyTo_CopiesSegment()
	{
		var source = KeyedArraySegment();
		var destination = new KeyedArraySegment<int, int>(new(3, Comparer<int>.Default));

		source.CopyTo(destination);

		Assert.NotSame(source.Array, destination.Array);
		Assert.Equal(source.Length, destination.Length);
		Assert.Equal(source.Elements.ToArray(), destination.Elements.ToArray());
		Assert.Equal(source.Keys.ToArray(), destination.Keys.ToArray());
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.Clone"/> clones the segment.
	/// </summary>
	[Fact]
	public void Clone_ClonesSegment()
	{
		var expected = KeyedArraySegment();
		var actual = expected.Clone();

		Assert.NotSame(expected.Array, actual);
		Assert.Equal(expected.Length, actual.Length);
		Assert.Equal(expected.Elements.ToArray(), actual.Elements);
		Assert.Equal(expected.Keys.ToArray(), actual.Keys);
	}

	/// <summary>
	/// Tests if <see cref="KeyedArraySegment{TElement,TKey}.GetEnumerator()"/> returns the elements of the array.
	/// </summary>
	[Fact]
	public void GetEnumerator_ReturnsElements()
	{
		var segment = KeyedArraySegment();
		Assert.Equal(segment.Elements.ToArray(), segment);
	}

	private static KeyedArray<int, int> KeyedArray(int n = 5)
	{
		return new(Enumerable.Range(1, n).ToArray(), x => x, Comparer<int>.Default);
	}

	private static KeyedArraySegment<int, int> KeyedArraySegment(int n = 3)
	{
		var array = KeyedArray(n + 2);
		return new(array, 1, n);
	}
}
