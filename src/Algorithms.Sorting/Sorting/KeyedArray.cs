using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace Algorithms.Sorting.Sorting;

/// <summary>
/// Keyed array of elements of type <typeparamref name="TElement"/> and <typeparamref name="TKey"/>.
/// </summary>
/// <typeparam name="TElement">The type of elements in the <see cref="KeyedArray{TSource, TKey}"/>.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the <see cref="KeyedArray{TSource, TKey}"/>.</typeparam>
internal class KeyedArray<TElement, TKey> : IEnumerable<TElement>
{
	private TElement[] _elements;
	private TKey[] _keys;

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyedArray{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="elements">The source array.</param>
	/// <param name="keySelector">A function to extract the key from each element in the <see cref="IEnumerable{T}"/>.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare the keys.</param>
	public KeyedArray(TElement[] elements, Func<TElement, TKey> keySelector, IComparer<TKey> comparer)
	{
		_elements = elements;
		_keys = elements.Select(keySelector).ToArray();
		Comparer = comparer;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyedArray{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="size">The size of the array.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare the keys.</param>
	/// <exception cref="ArgumentOutOfRangeException">Size is less than zero.</exception>
	public KeyedArray(int size, IComparer<TKey> comparer)
	{
		if (size < 0)
			throw new ArgumentOutOfRangeException(nameof(size), size, "Size is less than zero.");

		_elements = new TElement[size];
		_keys = new TKey[size];
		Comparer = comparer;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyedArray{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="array">The array to copy.</param>
	public KeyedArray(KeyedArray<TElement, TKey> array)
	{
		_elements = new TElement[array.Length];
		_keys = new TKey[array.Length];
		Comparer = array.Comparer;

		array._elements.CopyTo(_elements.AsSpan());
		array._keys.CopyTo(_keys.AsSpan());
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="KeyedArray{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="array">The array to copy.</param>
	/// <param name="offset">The zero-based index of the first element in the range.</param>
	/// <param name="count">The number of elements in the range.</param>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Offset is outside the bounds of the original array or offset + count is greater than the length of the array.
	/// </exception>
	public KeyedArray(KeyedArray<TElement, TKey> array, int offset, int count)
	{
		if (offset < 0 || offset > array.Length)
			throw new ArgumentOutOfRangeException(nameof(offset), offset, "Range is outside the bounds of the array.");

		if (offset + count > array.Length)
			throw new ArgumentOutOfRangeException(nameof(count), "Range is outside the bounds of the array.");

		_elements = new TElement[count];
		_keys = new TKey[count];
		Comparer = array.Comparer;

		array._elements.AsSpan(offset, count).CopyTo(_elements);
		array._keys.AsSpan(offset, count).CopyTo(_keys);
	}

	/// <summary>
	/// Gets the elements of the array.
	/// </summary>
	public TElement[] Elements => _elements;

	/// <summary>
	/// Gets the keys of the <see cref="KeyedArray{TSource, TKey}"/>.
	/// </summary>
	public TKey[] Keys => _keys;

	/// <summary>
	/// Gets the <see cref="IComparer{T}"/> to compare the keys.
	/// </summary>
	public IComparer<TKey> Comparer { get; }

	/// <summary>
	/// Gets the length of the <see cref="KeyedArray{TSource, TKey}"/>.
	/// </summary>
	public int Length => _elements.Length;

	/// <summary>
	/// Gets or sets the element at the specified index.
	/// </summary>
	/// <param name="i">The index.</param>
	public (TElement Element, TKey Key) this[int i]
	{
		get => (_elements[i], _keys[i]);
		set => (_elements[i], _keys[i]) = (value.Element, value.Key);
	}

	/// <summary>
	/// Gets the element at the specified index.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <returns>The element at the specified index.</returns>
	public TElement Element(int i)
	{
		return _elements[i];
	}

	/// <summary>
	/// Gets the element at the specified index.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <returns>The element at the specified index.</returns>
	public TElement Element(Index i)
	{
		return _elements[i];
	}

	/// <summary>
	/// Gets the key at the specified index.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <returns>The key at the specified index.</returns>
	public TKey Key(int i)
	{
		return _keys[i];
	}

	/// <summary>
	/// Gets the key at the specified index.
	/// </summary>
	/// <param name="i">The index.</param>
	/// <returns>The key at the specified index.</returns>
	public TKey Key(Index i)
	{
		return _keys[i];
	}

	/// <summary>
	/// Compare the keys at the specified indices.
	/// </summary>
	/// <param name="i">The first index.</param>
	/// <param name="j">The second index.</param>
	/// <returns>The result of the comparison.</returns>
	public int Compare(int i, int j)
	{
		return Comparer.Compare(_keys[i], _keys[j]);
	}

	/// <summary>
	/// Compare the keys at the specified indices.
	/// </summary>
	/// <param name="i">The first index.</param>
	/// <param name="j">The second index.</param>
	/// <returns>The result of the comparison.</returns>
	public int Compare(Index i, Index j)
	{
		return Comparer.Compare(_keys[i], _keys[j]);
	}

	/// <summary>
	/// Compare the key at the specified index with the specified key.
	/// </summary>
	/// <param name="i">The index of the first key.</param>
	/// <param name="key">The key.</param>
	/// <returns>The result of the comparison.</returns>
	public int Compare(int i, TKey key)
	{
		return Comparer.Compare(_keys[i], key);
	}

	/// <summary>
	/// Compare the key at the specified index with the specified key.
	/// </summary>
	/// <param name="i">The index of the first key.</param>
	/// <param name="key">The key.</param>
	/// <returns>The result of the comparison.</returns>
	public int Compare(Index i, TKey key)
	{
		return Comparer.Compare(_keys[i], key);
	}

	/// <summary>
	/// Swaps the elements at the specified indices.
	/// </summary>
	/// <param name="i">The first index.</param>
	/// <param name="j">The second index.</param>
	public void Swap(int i, int j)
	{
		if (i == j)
			return;

		(_elements[i], _elements[j]) = (_elements[j], _elements[i]);
		(_keys[i], _keys[j]) = (_keys[j], _keys[i]);
	}

	/// <summary>
	/// Swaps the elements at the specified indices.
	/// </summary>
	/// <param name="i">The first index.</param>
	/// <param name="j">The second index.</param>
	public void Swap(Index i, Index j)
	{
		Swap(i.GetOffset(Length), j.GetOffset(Length));
	}

	/// <summary>
	/// Forms a slice of the specified length out of the current array starting at the specified index.
	/// </summary>
	/// <param name="index">The index at which to begin the slice.</param>
	/// <param name="count">The desired length of the slice.</param>
	/// <returns>The slice of the current array.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Index is outside the bounds of the original array or index + count is greater than the length of the array.
	/// </exception>
	public KeyedArraySegment<TElement, TKey> Slice(int index, int count)
	{
		return new(this, index, count);
	}

	/// <summary>
	/// Resizes the array to the specified new size.
	/// </summary>
	/// <param name="newSize">The new size of the array.</param>
	public void Resize(int newSize)
	{
		Array.Resize(ref _elements, newSize);
		Array.Resize(ref _keys, newSize);
	}

	/// <summary>
	/// Copies the elements in the current <see cref="KeyedArray{TElement,TKey}"/> to the specified
	/// <see cref="KeyedArraySegment{TElement,TKey}"/>.
	/// </summary>
	/// <param name="destination">The destination <see cref="KeyedArraySegment{TElement,TKey}"/> to copy to.</param>
	public void CopyTo(KeyedArraySegment<TElement, TKey> destination)
	{
		Elements.CopyTo(destination.Elements);
		Keys.CopyTo(destination.Keys);
	}

	/// <summary>
	/// Clones the current <see cref="KeyedArray{TSource, TKey}"/>.
	/// </summary>
	/// <returns>Cloned <see cref="KeyedArray{TSource, TKey}"/>.</returns>
	public KeyedArray<TElement, TKey> Clone()
	{
		return new(this);
	}

	/// <inheritdoc />
	public IEnumerator<TElement> GetEnumerator()
	{
		return _elements.AsEnumerable().GetEnumerator();
	}

	/// <inheritdoc />
	[ExcludeFromCodeCoverage]
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

/// <summary>
/// Extension methods for <see cref="IEnumerable{T}"/>.
/// </summary>
internal static class KeyedArrayEnumerableExtensions
{
	/// <summary>
	/// Creates a <see cref="KeyedArray{TSource, TKey}"/> from an <see cref="IEnumerable{T}"/>.
	/// </summary>
	/// <param name="source">An <see cref="IEnumerable{T}"/> to create a <see cref="KeyedArray{TSource, TKey}"/> from.</param>
	/// <param name="keySelector">A function to extract the key from each element in the <see cref="IEnumerable{T}"/>.</param>
	/// <param name="comparer">An <see cref="IComparer{T}"/> to compare the keys.</param>
	/// <typeparam name="TSource">The type of elements in <paramref name="source"/>.</typeparam>
	/// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
	/// <returns>A <see cref="KeyedArray{TSource, TKey}"/> that contains the elements of <paramref name="source"/>.</returns>
	public static KeyedArray<TSource, TKey> ToKeyedArray<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new(source.ToArray(), keySelector, comparer);
	}
}
