// Stryker disable all : Stryker cannot compile this source file.

using System.Numerics;
using Algorithms.Sorting.Sorting;
using Algorithms.Sorting.Utilities;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the Most Significant Digit-<a href="https://en.wikipedia.org/wiki/Radix_sort">Radix Sort</a> sorting algorithm.
/// </summary>
public static class MsdRadixSort
{
	private static int Sort<TSource, TKey>(KeyedArraySegment<TSource, TKey> source, TKey bit, bool reverse)
		where TKey : IBinaryInteger<TKey>
	{
		if (bit == TKey.Zero || source.Length <= 1)
			return 0;

		var temp = new KeyedArray<TSource, TKey>(source.Length, source.Array.Comparer);
		var j = 0;
		for (var i = 0; i < source.Length; i++)
		{
			if ((source.Key(i) & bit) == TKey.Zero ^ reverse)
				source[i - j] = source[i];
			else
				temp[j++] = source[i];
		}

		temp[..j].CopyTo(source[^j..]);

		bit >>>= 1;
		Sort(source[..^j], bit, reverse);
		Sort(source[^j..], bit, reverse);

		return reverse ? j : source.Length - j;
	}

	/// <summary>
	/// Represents the Most Significant Digit-<a href="https://en.wikipedia.org/wiki/Radix_sort">Radix Sort</a>
	/// algorithm for integers.
	/// </summary>
	/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
	public sealed class Integer<TKey> : ISortingAlgorithm<TKey> where TKey : IBinaryInteger<TKey>, IMinMaxValue<TKey>
	{
		private readonly int _radixInt;
		private readonly TKey _radix;

		/// <inheritdoc />
		public Integer() : this(2) { }

		/// <summary>
		/// Initializes a new instance of the <see cref="Integer{TKey}"/> class.
		/// </summary>
		/// <param name="radix">The radix.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="radix"/> is less than two.</exception>
		public Integer(int radix)
		{
			if (radix < 2)
				throw new ArgumentOutOfRangeException(nameof(radix), radix, "The radix must be at least 2.");

			_radixInt = radix;
			_radix = TKey.CreateChecked(radix);
		}

		/// <inheritdoc />
		public static ISortingAlgorithm<TKey> Default { get; } = new Integer<TKey>();

		/// <inheritdoc />
		public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			var reverse = comparer.Compare(TKey.Zero, TKey.One) > 0;

			KeyedArray<TSource, TKey> keyedArray;
			if (_radixInt == 2)
			{
				keyedArray = source.ToKeyedArray(x => keySelector(x) ^ TKey.MinValue, comparer);
				Sort<TSource, TKey>(keyedArray, TKey.One << TKey.Zero.GetByteCount() * 8 - 1, reverse);
				return keyedArray;
			}

			keyedArray = source.ToKeyedArray(keySelector, comparer);
			Sort(keyedArray, reverse);
			return keyedArray;
		}

		/// <inheritdoc />
		public ISortingAlgorithm<T> CreateComposite<T>()
		{
			throw new NotSupportedException();
		}

		private void Sort<TSource>(KeyedArray<TSource, TKey> source, bool reverse)
		{
			if (source.MinMax() is not var (min, max))
				return;

			var m = MaxDivisor(min);
			var n = MaxDivisor(max);

			if (TKey.IsPositive(min) && TKey.IsPositive(max))
			{
				Sort<TSource>(source, TKey.Max(m, n), reverse);
				return;
			}

			if (TKey.IsNegative(min) && TKey.IsNegative(max))
			{
				Sort<TSource>(source, TKey.Min(m, n), !reverse);
				return;
			}

			var temp = new KeyedArray<TSource, TKey>(source.Length, source.Comparer);
			var j = 0;
			for (var i = 0; i < source.Length; i++)
			{
				if (TKey.IsNegative(source.Key(i)) ^ reverse)
					source[i - j] = source[i];
				else
					temp[j++] = source[i];
			}

			temp[..j].CopyTo(source[^j..]);

			Sort(source[..^j], m, true);
			Sort(source[^j..], n, false);
		}

		private void Sort<TSource>(KeyedArraySegment<TSource, TKey> source, TKey n, bool reverse)
		{
			if (source.Length <= 1 || n == TKey.Zero)
				return;

			var digits = new int[source.Length];
			var count = new int[_radixInt + 1];
			var zeros = 0;
			for (var i = 0; i < source.Length; i++)
			{
				var (element, key) = source[i];
				(var digit, key) = TKey.DivRem(key, n);
				if (key == TKey.Zero)
					zeros++;

				var digitInt = int.CreateChecked(digit);
				if (reverse)
					digitInt = _radixInt - digitInt - 1;

				digits[i] = digitInt;
				count[digitInt]++;
				source[i] = (element, key);
			}

			for (var i = 1; i < count.Length; i++)
				count[i] += count[i - 1];

			var temp = new KeyedArray<TSource, TKey>(source.Length, source.Array.Comparer);
			for (var i = 0; i < source.Length; i++)
				temp[--count[digits[i]]] = source[i];

			temp.CopyTo(source);

			if (zeros == source.Length)
				return;

			// BUG: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3708
#pragma warning disable SA1011
			for (var i = 0; i < count.Length - 1; i++)
				Sort(source[count[i]..count[i + 1]], n / _radix, reverse);
#pragma warning restore SA1011
		}

		private TKey MaxDivisor(TKey x)
		{
			TKey n;
			if (TKey.IsPositive(x))
			{
				n = TKey.One;
				for (x /= _radix; x > TKey.Zero; x /= _radix)
					n *= _radix;

				return n;
			}

			n = -TKey.One;
			for (x /= _radix; x < TKey.Zero; x /= _radix)
				n *= _radix;

			return n;
		}
	}

	/// <summary>
	/// Represents the Least Significant Digit-<a href="https://en.wikipedia.org/wiki/Radix_sort">Radix Sort</a> algorithm
	/// for floating point numbers.
	/// </summary>
	/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
	public sealed class FloatingPoint<TKey> : ISortingAlgorithm<TKey> where TKey : IFloatingPointIeee754<TKey>
	{
		/// <inheritdoc />
		public static ISortingAlgorithm<TKey> Default { get; } = new FloatingPoint<TKey>();

		/// <inheritdoc />
		public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			return TKey.Zero switch
			{
				Half => Sort(source, x => (short)(BitConverter.HalfToInt16Bits((Half)(object)keySelector(x)) ^ short.MinValue), comparer),
				float => Sort(source, x => BitConverter.SingleToInt32Bits((float)(object)keySelector(x)) ^ int.MinValue, comparer),
				double => Sort(source, x => BitConverter.DoubleToInt64Bits((double)(object)keySelector(x)) ^ long.MinValue, comparer),
				_ => throw new NotSupportedException(),
			};
		}

		/// <inheritdoc />
		public ISortingAlgorithm<T> CreateComposite<T>()
		{
			throw new NotSupportedException();
		}

		private static IEnumerable<TSource> Sort<TSource, TInteger>(IEnumerable<TSource> source, Func<TSource, TInteger> keySelector, IComparer<TKey> comparer)
			where TInteger : IBinaryInteger<TInteger>, IMinMaxValue<TInteger>
		{
			var reverse = comparer.Compare(TKey.Zero, TKey.One) > 0;
			var intComparer = reverse ? Comparer<TInteger>.Default.Reverse() : Comparer<TInteger>.Default;
			var keyedArray = source.ToKeyedArray(keySelector, intComparer);

			var bit = TInteger.One << TInteger.Zero.GetByteCount() * 8 - 1;
			var negative = MsdRadixSort.Sort<TSource, TInteger>(keyedArray, bit, reverse);

			var range = reverse ? ^negative.. : ..negative;
			keyedArray.Elements.AsSpan(range).Reverse();
			return keyedArray;
		}
	}

	/// <summary>
	/// Represents the Most Significant Digit-<a href="https://en.wikipedia.org/wiki/Radix_sort">Radix Sort</a>
	/// algorithm for strings.
	/// </summary>
	public sealed class String : ISortingAlgorithm<string>
	{
		/// <inheritdoc />
		public static ISortingAlgorithm<string> Default { get; } = new String();

		/// <inheritdoc />
		public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, string> keySelector, IComparer<string> comparer)
		{
			var keyedArray = source.ToKeyedArray(keySelector, comparer);
			var reverse = comparer.Compare("a", "b") > 0;
			Sort<TSource>(keyedArray, 0, reverse);
			return keyedArray;
		}

		/// <inheritdoc />
		public ISortingAlgorithm<T> CreateComposite<T>()
		{
			throw new NotSupportedException();
		}

		private static void Sort<TSource>(KeyedArraySegment<TSource, string> source, int d, bool reverse)
		{
			if (source.Length <= 1)
				return;

			var keys = new int[source.Length];
			var count = new int[byte.MaxValue + 2];
			for (var i = 0; i < source.Length; i++)
			{
				var c = source.Key(i).TryGetCharAt(d) ?? 0;
				if (c > byte.MaxValue)
					throw new ArgumentOutOfRangeException(nameof(source), (char)c, "The element at index " + i + " is not an ASCII character.");

				if (reverse)
					c = byte.MaxValue - c;

				keys[i] = c;
				count[c]++;
			}

			if (count[reverse ? byte.MaxValue : 0] == source.Length)
				return;

			for (var i = 1; i < count.Length; i++)
				count[i] += count[i - 1];

			var temp = new KeyedArray<TSource, string>(source.Length, source.Array.Comparer);
			for (var i = 0; i < source.Length; i++)
				temp[--count[keys[i]]] = source[i];

			temp.CopyTo(source);

			// BUG: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3708
#pragma warning disable SA1011
			for (var i = 0; i < count.Length - 1; i++)
				Sort(source[count[i]..count[i + 1]], d + 1, reverse);
#pragma warning restore SA1011
		}
	}
}
