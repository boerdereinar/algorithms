using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Algorithms.Common.Collections;

/// <summary>
/// Delimits a section of a <see cref="KeyedArray{TElement,TKey}"/>.
/// </summary>
/// <typeparam name="TElement">The type of elements in the <see cref="KeyedArray{TElement,TKey}"/>.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the <see cref="KeyedArray{TElement,TKey}"/>.</typeparam>
public readonly struct KeyedArraySegment<TElement, TKey> : IEnumerable<TElement>
{
	private const string IndexOutOfRange = "Index is outside the bounds of the segment.";

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyedArraySegment{TSource, TKey}"/> struct.
	/// </summary>
	/// <param name="array">The array to wrap.</param>
	public KeyedArraySegment(KeyedArray<TElement, TKey> array) : this(array, 0, array.Length) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyedArraySegment{TSource, TKey}"/> struct.
	/// </summary>
	/// <param name="array">The array to wrap.</param>
	/// <param name="offset">The zero-based index of the first element in the range.</param>
	/// <param name="count">The number of elements in the range.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Offset is outside the bounds of the original array or offset + count is greater than the length of the array.
	/// </exception>
	public KeyedArraySegment(KeyedArray<TElement, TKey> array, int offset, int count)
	{
		if (offset < 0 || offset > array.Length)
			throw new ArgumentOutOfRangeException(nameof(offset), offset, "Range is outside the bounds of the array.");

		if (offset + count > array.Length)
			throw new ArgumentOutOfRangeException(nameof(count), "Range is outside the bounds of the array.");

		Array = array;
		Offset = offset;
		Length = count;
	}

	/// <summary>
	/// Gets the source array.
	/// </summary>
	public KeyedArray<TElement, TKey> Array { get; }

	/// <summary>
	/// Gets the source of the <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	public Span<TElement> Elements => Array.Elements.AsSpan(Offset, Length);

	/// <summary>
	/// Gets the keys of the <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	public Span<TKey> Keys => Array.Keys.AsSpan(Offset, Length);

	/// <summary>
	/// Gets the zero-based index of the first element in the range.
	/// </summary>
	public int Offset { get; }

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Length"/>
	public int Length { get; }

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.this"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public (TElement Element, TKey Key) this[int i]
	{
		get
		{
			if (i < 0 || i >= Length)
				throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);
			return Array[i + Offset];
		}

		set
		{
			if (i < 0 || i >= Length)
				throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);
			Array[i + Offset] = value;
		}
	}

	/// <summary>
	/// Implicit conversion from <see cref="KeyedArray{TElement,TKey}"/> to <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	/// <param name="array">The array to convert.</param>
	/// <returns>The converted <see cref="KeyedArraySegment{TElement,TKey}"/>.</returns>
	public static implicit operator KeyedArraySegment<TElement, TKey>(KeyedArray<TElement, TKey> array)
	{
		return new(array);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Element(int)"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public TElement Element(int i)
	{
		if (i < 0 || i >= Length)
			throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);
		return Array.Element(i + Offset);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Element(int)"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public TElement Element(Index i)
	{
		return Element(i.GetOffset(Length));
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Key(int)"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public TKey Key(int i)
	{
		if (i < 0 || i >= Length)
			throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);
		return Array.Key(i + Offset);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Key(Index)"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public TKey Key(Index i)
	{
		return Key(i.GetOffset(Length));
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Compare(int,int)"/>
	/// <exception cref="ArgumentOutOfRangeException">i or j is outside the bounds of the segment.</exception>
	public int Compare(int i, int j)
	{
		if (i < 0 || i >= Length)
			throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);
		if (j < 0 || j >= Length)
			throw new ArgumentOutOfRangeException(nameof(j), j, IndexOutOfRange);

		return Array.Compare(i + Offset, j + Offset);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Compare(Index,Index)"/>
	/// <exception cref="ArgumentOutOfRangeException">i or j is outside the bounds of the segment.</exception>
	public int Compare(Index i, Index j)
	{
		return Compare(i.GetOffset(Length), j.GetOffset(Length));
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Compare(int,TKey)"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public int Compare(int i, TKey key)
	{
		if (i < 0 || i >= Length)
			throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);

		return Array.Compare(i + Offset, key);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Compare(Index,TKey)"/>
	/// <exception cref="ArgumentOutOfRangeException">i is outside the bounds of the segment.</exception>
	public int Compare(Index i, TKey key)
	{
		return Compare(i.GetOffset(Length), key);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Swap(int,int)"/>
	/// <exception cref="ArgumentOutOfRangeException">i or j is outside the bounds of the segment.</exception>
	public void Swap(int i, int j)
	{
		if (i < 0 || i >= Length)
			throw new ArgumentOutOfRangeException(nameof(i), i, IndexOutOfRange);
		if (j < 0 || j >= Length)
			throw new ArgumentOutOfRangeException(nameof(j), j, IndexOutOfRange);

		Array.Swap(i + Offset, j + Offset);
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Swap(Index,Index)"/>
	/// <exception cref="ArgumentOutOfRangeException">i or j is outside the bounds of the segment.</exception>
	public void Swap(Index i, Index j)
	{
		Swap(i.GetOffset(Length), j.GetOffset(Length));
	}

	/// <inheritdoc cref="KeyedArray{TElement,TKey}.Slice(int,int)"/>
	public KeyedArraySegment<TElement, TKey> Slice(int index, int count)
	{
		if (index < 0 || index > Length)
			throw new ArgumentOutOfRangeException(nameof(index), index, IndexOutOfRange);

		if (index + count > Length)
			throw new ArgumentOutOfRangeException(nameof(count), IndexOutOfRange);

		return new(Array, Offset + index, count);
	}

	/// <summary>
	/// Copies the elements in the current <see cref="KeyedArraySegment{TElement,TKey}"/> to the specified
	/// <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	/// <param name="destination">The destination <see cref="KeyedArraySegment{TElement,TKey}"/> to copy to.</param>
	public void CopyTo(KeyedArraySegment<TElement, TKey> destination)
	{
		Elements.CopyTo(destination.Elements);
		Keys.CopyTo(destination.Keys);
	}

	/// <summary>
	/// Clones the elements in the current <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	/// <returns>Cloned <see cref="KeyedArraySegment{TElement,TKey}"/>.</returns>
	public KeyedArray<TElement, TKey> Clone()
	{
		return new(Array, Offset, Length);
	}

	/// <inheritdoc />
	public IEnumerator<TElement> GetEnumerator()
	{
		for (var i = 0; i < Length; i++)
			yield return Array.Elements[i + Offset];
	}

	/// <inheritdoc />
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}
