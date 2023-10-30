using System.Diagnostics.CodeAnalysis;
using Algorithms.Sorting.DataStructures.Trees;

namespace Algorithms.Sorting.DataStructures.Heaps;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Heap</a> data structure.
/// </summary>
public sealed class LeftistHeap : IHeap
{
	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return LeftistHeap<TSource, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Heap</a> data structure.
/// </summary>
/// <typeparam name="TSource">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class LeftistHeap<TSource, TKey> : IHeap<TSource, TKey>
{
	private readonly IComparer<TKey> _comparer;

	private LeftistTree<TSource, TKey>? _tree;

	/// <summary>
	/// Initializes a new instance of the <see cref="LeftistHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public LeftistHeap(IComparer<TKey> comparer)
	{
		_comparer = comparer;
		_tree = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="LeftistHeap{TSource, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public LeftistHeap(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		_comparer = comparer;

		var sourceArray = source.ToArray();
		Count = sourceArray.Length;
		_tree = LeftistTree<TSource, TKey>.Create(sourceArray, keySelector, comparer);
	}

	/// <inheritdoc />
	public int Count { get; private set; }

	/// <inheritdoc />
	public TSource Min => _tree is not null ? _tree.Value : throw new InvalidOperationException("The heap is empty.");

	/// <inheritdoc />
	public void Insert(TSource value, TKey key)
	{
		_tree = LeftistTree<TSource, TKey>.Merge(_tree, new(value, key, _comparer));
		Count++;
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		if (_tree is null)
		{
			value = default;
			key = default;
			return false;
		}

		value = _tree.Value;
		key = _tree.Key;
		return true;
	}

	/// <inheritdoc />
	public TSource DeleteMin()
	{
		return TryDeleteMin(out var value, out _) ? value : throw new InvalidOperationException("The heap is empty.");
	}

	/// <inheritdoc />
	public bool TryDeleteMin([MaybeNullWhen(false)] out TSource value, [MaybeNullWhen(false)] out TKey key)
	{
		if (_tree is null)
		{
			value = default;
			key = default;
			return false;
		}

		value = _tree.Value;
		key = _tree.Key;
		_tree = LeftistTree<TSource, TKey>.Merge(_tree.Left, _tree.Right);
		Count--;
		return true;
	}

	/// <inheritdoc />
	public static IHeap<TSource, TKey> Create(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new LeftistHeap<TSource, TKey>(source, keySelector, comparer);
	}
}
