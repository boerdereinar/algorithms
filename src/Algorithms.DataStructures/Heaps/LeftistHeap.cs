using System.Diagnostics.CodeAnalysis;
using Algorithms.DataStructures.Trees;

namespace Algorithms.DataStructures.Heaps;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Heap</a> data structure.
/// </summary>
public sealed class LeftistHeap : IHeap
{
	[ExcludeFromCodeCoverage]
	private LeftistHeap() { }

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create<TValue, TKey>(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return LeftistHeap<TValue, TKey>.Create(source, keySelector, comparer);
	}
}

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Leftist_tree">Leftist Heap</a> data structure.
/// </summary>
/// <typeparam name="TValue">The type of the elements in the heap.</typeparam>
/// <typeparam name="TKey">The type of the key used to compare elements in the heap.</typeparam>
public sealed class LeftistHeap<TValue, TKey> : IHeap<TValue, TKey>
{
	private readonly IComparer<TKey> _comparer;

	private LeftistTree<TValue, TKey>? _tree;

	/// <summary>
	/// Initializes a new instance of the <see cref="LeftistHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public LeftistHeap(IComparer<TKey> comparer)
	{
		_comparer = comparer;
		_tree = null;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="LeftistHeap{TValue, TKey}"/> class.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="keySelector">The function used to extract the key from the element.</param>
	/// <param name="comparer">The comparer used to compare keys.</param>
	public LeftistHeap(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		_comparer = comparer;

		var sourceArray = source.ToArray();
		Count = sourceArray.Length;
		_tree = LeftistTree<TValue, TKey>.Create(sourceArray, keySelector, comparer);
	}

	/// <inheritdoc />
	public int Count { get; private set; }

	/// <inheritdoc />
	public TValue Min => _tree is not null ? _tree.Value : throw new InvalidOperationException("The heap is empty.");

	/// <inheritdoc />
	public void Insert(TValue value, TKey key)
	{
		_tree = LeftistTree<TValue, TKey>.Merge(_tree, new(value, key, _comparer));
		Count++;
	}

	/// <inheritdoc />
	public bool TryGetMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key)
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
	public TValue DeleteMin()
	{
		return TryDeleteMin(out var value, out _) ? value : throw new InvalidOperationException("The heap is empty.");
	}

	/// <inheritdoc />
	public bool TryDeleteMin([MaybeNullWhen(false)] out TValue value, [MaybeNullWhen(false)] out TKey key)
	{
		if (_tree is null)
		{
			value = default;
			key = default;
			return false;
		}

		value = _tree.Value;
		key = _tree.Key;
		_tree = LeftistTree<TValue, TKey>.Merge(_tree.Left, _tree.Right);
		Count--;
		return true;
	}

	/// <inheritdoc />
	public static IHeap<TValue, TKey> Create(IEnumerable<TValue> source, Func<TValue, TKey> keySelector, IComparer<TKey> comparer)
	{
		return new LeftistHeap<TValue, TKey>(source, keySelector, comparer);
	}
}
