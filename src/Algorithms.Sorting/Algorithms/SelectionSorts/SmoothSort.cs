using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.SelectionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Smoothsort">Smoothsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class SmoothSort<TKey> : ISortingAlgorithm<TKey>
{
	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new SmoothSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		if (keyedArray.Length <= 1)
			return keyedArray;

		UInt128 p = 1;
		var b = new Leonardo();
		var n = keyedArray.Length;

		for (var q = 0; ++q < n; p++)
		{
			if (p % 8 == 3)
			{
				Sift(keyedArray, q - 1, b);
				b++;
				b++;
				p >>= 2;
			}
			else if (p % 4 == 1)
			{
				if (q + ~b < n)
					Sift(keyedArray, q - 1, b);
				else
					Trinkle(keyedArray, q - 1, p, b);

				for (p <<= 1; --b > 1; p <<= 1) { }
			}
		}

		Trinkle(keyedArray, n - 1, p, b);

		for (--p; n-- > 1; p--)
		{
			if (b == 1)
				for (; p % 2 == 0; p >>= 1)
					b++;
			else if (b >= 3)
			{
				if (p != 0)
					SemiTrinkle(keyedArray, n - b.Gap, p, b);

				b--;
				p = (p << 1) + 1;
				SemiTrinkle(keyedArray, n - 1, p, b);
				b--;
				p = (p << 1) + 1;
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new SmoothSort<T>();
	}

	private static void Sift<TSource>(KeyedArray<TSource, TKey> source, int r, Leonardo b)
	{
		while (b >= 3)
		{
			int r2;
			if (source.Compare(r - b.Gap, r - 1) >= 0)
				r2 = r - b.Gap;
			else
			{
				r2 = r - 1;
				b--;
			}

			if (source.Compare(r, r2) >= 0)
				break;

			source.Swap(r, r = r2);
			b--;
		}
	}

	private static void SemiTrinkle<TSource>(KeyedArray<TSource, TKey> source, int r, UInt128 p, Leonardo b)
	{
		if (source.Compare(r - ~b, r) < 0)
			return;

		source.Swap(r, r - ~b);
		Trinkle(source, r - ~b, p, b);
	}

	private static void Trinkle<TSource>(KeyedArray<TSource, TKey> source, int r, UInt128 p, Leonardo b)
	{
		while (p > 0)
		{
			for (; p % 2 == 0; p >>= 1)
				b++;

			if (--p == 0 || source.Compare(r, r - b) >= 0)
				break;

			if (b == 1)
				source.Swap(r, r -= b);
			else if (b >= 3)
			{
				var r2 = r - b.Gap;
				var r3 = r - b;

				if (source.Compare(r - 1, r2) >= 0)
				{
					r2 = r - 1;
					p <<= 1;
					b--;
				}

				if (source.Compare(r3, r2) >= 0)
					source.Swap(r, r = r3);
				else
				{
					source.Swap(r, r = r2);
					b--;
					break;
				}
			}
		}

		Sift(source, r, b);
	}

	/// <summary>
	/// Represents a <a href="https://en.wikipedia.org/wiki/Leonardo_number">Leonardo number</a>.
	/// </summary>
	private record Leonardo()
	{
		private readonly int _a = 1;
		private readonly int _b = 1;

		private Leonardo(int a, int b) : this()
		{
			_a = a;
			_b = b;
		}

		/// <summary>
		/// Gets the gap between the current and previous Leonardo number.
		/// </summary>
		public int Gap => _a - _b;

		public static implicit operator int(Leonardo x)
		{
			return x._a;
		}

		public static int operator ~(Leonardo x)
		{
			return x._b;
		}

		public static Leonardo operator ++(Leonardo x)
		{
			return new(x._a + x._b + 1, x._a);
		}

		public static Leonardo operator --(Leonardo x)
		{
			return new(x._b, x._a - x._b - 1);
		}
	}
}
