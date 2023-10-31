// Stryker disable all : Stryker cannot compile this source file.

using System.Numerics;
using Algorithms.Common.Collections;
using Algorithms.Common.Comparers;
using Algorithms.Common.Utilities;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/American_flag_sort">American Flag Sort</a> sorting algorithm.
/// </summary>
public static class AmericanFlagSort
{
	private static int Sort<TSource, TKey>(KeyedArraySegment<TSource, TKey> source, TKey bit, bool reverse)
		where TKey : IBinaryInteger<TKey>
	{
		if (bit == TKey.Zero || source.Length <= 1)
			return 0;

		var l = 0;
		var r = source.Length - 1;
		while (true)
		{
			while (l < r && (source.Key(l) & bit) == TKey.Zero ^ reverse) l++;
			while (l < r && (source.Key(r) & bit) != TKey.Zero ^ reverse) r--;

			if (l >= r)
				break;

			source.Swap(l++, r--);
		}

		if (l < source.Length && (source.Key(l) & bit) == TKey.Zero ^ reverse)
			l++;

		bit >>>= 1;

		Sort(source[..l], bit, reverse);
		Sort(source[l..], bit, reverse);

		return reverse ? source.Length - l : l;
	}

	/// <summary>
	/// Represents the <a href="https://en.wikipedia.org/wiki/American_flag_sort">American Flag Sort</a> sorting algorithm
	/// for integers.
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

			var l = 0;
			var r = source.Length - 1;
			while (true)
			{
				while (l < r && TKey.IsNegative(source.Key(l)) ^ reverse) l++;
				while (l < r && TKey.IsPositive(source.Key(r)) ^ reverse) r--;

				if (l >= r)
					break;

				source.Swap(l, r);
			}

			Sort(source[..l], m, true);
			Sort(source[l..], n, false);
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
				count[digitInt + 1]++;
				source[i] = (element, key);
			}

			for (var i = 2; i < count.Length; i++)
				count[i] += count[i - 1];

			var offset = new int[_radixInt + 1];
			count.CopyTo(offset.AsSpan());

			for (var block = 0; block < _radixInt;)
			{
				var i = count[block];
				if (i >= offset[block + 1])
				{
					block++;
					continue;
				}

				var digit = digits[i];
				if (digit != block)
				{
					var j = count[digit];
					source.Swap(i, count[digit]);
					(digits[i], digits[j]) = (digits[j], digits[i]);
				}

				count[digit]++;
			}

			if (zeros == source.Length)
				return;

			// BUG: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3708
#pragma warning disable SA1011
			for (var i = 0; i < count.Length - 1; i++)
				Sort(source[offset[i]..offset[i + 1]], n / _radix, reverse);
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
	/// Represents the <a href="https://en.wikipedia.org/wiki/American_flag_sort">American Flag Sort</a> sorting algorithm
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
			var negative = AmericanFlagSort.Sort<TSource, TInteger>(keyedArray, bit, reverse);

			var range = reverse ? ^negative.. : ..negative;
			keyedArray.Elements.AsSpan(range).Reverse();
			return keyedArray;
		}
	}

	/// <summary>
	/// Represents the <a href="https://en.wikipedia.org/wiki/American_flag_sort">American Flag Sort</a> sorting algorithm
	/// for strings.
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
				count[c + 1]++;
			}

			if (count[reverse ? byte.MaxValue + 1 : 1] == source.Length)
				return;

			for (var i = 2; i < count.Length; i++)
				count[i] += count[i - 1];

			var offset = new int[byte.MaxValue + 2];
			count.CopyTo(offset.AsSpan());

			for (var block = 0; block <= byte.MaxValue;)
			{
				var i = count[block];
				if (i >= offset[block + 1])
				{
					block++;
					continue;
				}

				var key = keys[i];
				if (key != block)
				{
					var j = count[key];
					source.Swap(i, count[key]);
					(keys[i], keys[j]) = (keys[j], keys[i]);
				}

				count[key]++;
			}

			// BUG: https://github.com/DotNetAnalyzers/StyleCopAnalyzers/issues/3708
#pragma warning disable SA1011
			for (var i = 0; i < count.Length - 1; i++)
				Sort(source[offset[i]..offset[i + 1]], d + 1, reverse);
#pragma warning restore SA1011
		}
	}
}
