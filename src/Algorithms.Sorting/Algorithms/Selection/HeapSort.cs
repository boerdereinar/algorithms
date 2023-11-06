using System.Diagnostics.CodeAnalysis;
using Algorithms.DataStructures.Heaps;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Selection;

/// <summary>
/// Represents a generic <a href="https://en.wikipedia.org/wiki/Heapsort">Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
/// <typeparam name="THeap">The type of the heap.</typeparam>
public class HeapSort<TKey, THeap> : ISortingAlgorithm<TKey> where THeap : IHeap
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new HeapSort<TKey, THeap>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var heap = THeap.Create(source, keySelector, comparer);
		while (heap.TryDeleteMin(out var value, out _))
			yield return value;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new HeapSort<T, THeap>();
	}
}

/// <summary>
/// Represents a generic <a href="https://en.wikipedia.org/wiki/Heapsort">Heapsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
/// <typeparam name="THeap">The type of the heap.</typeparam>
/// <typeparam name="TArgument">The type of the argument of the heap.</typeparam>
public class HeapSort<TKey, THeap, TArgument> : ISortingAlgorithm<TKey> where THeap : IHeap<TArgument>
{
	/// <summary>
	/// Gets the argument of the heap sort.
	/// </summary>
	public TArgument? Argument { get; }

	/// <summary>
	/// Gets a value indicating whether the heap sort has an argument.
	/// </summary>
	[MemberNotNullWhen(true, nameof(Argument))]
	public bool HasArgument { get; }

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new HeapSort<TKey, THeap, TArgument>();

	/// <summary>
	/// Initializes a new instance of the <see cref="HeapSort{TKey, THeap, TArgument}"/> class.
	/// </summary>
	public HeapSort() { }

	/// <summary>
	/// Initializes a new instance of the <see cref="HeapSort{TKey, THeap, TArgument}"/> class.
	/// </summary>
	/// <param name="argument">The argument of the heap.</param>
	public HeapSort(TArgument argument)
	{
		Argument = argument;
		HasArgument = true;
	}

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var heap = HasArgument
			? THeap.Create(Argument, source, keySelector, comparer)
			: THeap.Create(source, keySelector, comparer);

		while (heap.TryDeleteMin(out var value, out _))
			yield return value;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return HasArgument ? new HeapSort<T, THeap, TArgument>(Argument) : new();
	}
}
