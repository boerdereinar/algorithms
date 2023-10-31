// Stryker disable all : Stryker cannot compile this source file.

using System.Numerics;
using Algorithms.Common.Collections;
using Algorithms.Common.Comparers;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.Distribution;

/// <summary>
/// Represents the Least Significant Digit-<a href="https://en.wikipedia.org/wiki/Radix_sort">Radix Sort</a> sorting algorithm.
/// </summary>
public static class LsdRadixSort
{
	private static int Sort<TSource, TKey>(KeyedArray<TSource, TKey> source, bool reverse)
		where TKey : IBinaryInteger<TKey>
	{
		var j = 0;
		var temp = new KeyedArray<TSource, TKey>(source.Length, source.Comparer);
		for (var bit = TKey.One; bit != TKey.Zero; bit <<= 1)
		{
			j = 0;
			for (var i = 0; i < source.Length; i++)
			{
				if ((source.Key(i) & bit) == TKey.Zero ^ reverse)
					source[i - j] = source[i];
				else
					temp[j++] = source[i];
			}

			temp[..j].CopyTo(source[^j..]);
		}

		return reverse ? j : source.Length - j;
	}

	/// <summary>
	/// Represents the Least Significant Digit-<a href="https://en.wikipedia.org/wiki/Radix_sort">Radix Sort</a> algorithm
	/// for integers.
	/// </summary>
	/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
	public sealed class Integer<TKey> : ISortingAlgorithm<TKey> where TKey : IBinaryInteger<TKey>, IMinMaxValue<TKey>
	{
		private readonly int _radixInt;
		private readonly TKey _radix;
		private readonly int _offset;

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
			_offset = typeof(TKey).GetInterface(typeof(ISignedNumber<>).Name) is null ? 0 : radix - 1;
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
				LsdRadixSort.Sort(keyedArray, reverse);
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
			var digits = new int[source.Length];
			var temp = new KeyedArray<TSource, TKey>(source.Length, source.Comparer);
			for (var zeros = 0; zeros != source.Length;)
			{
				zeros = 0;
				var offsets = new int[_radixInt + _offset + 1];
				for (var i = 0; i < source.Length; i++)
				{
					var (element, key) = source[i];
					(key, var digit) = TKey.DivRem(key, _radix);
					if (key == TKey.Zero)
						zeros++;

					var digitInt = int.CreateChecked(digit) + _offset;
					if (reverse)
						digitInt = _radixInt + _offset - digitInt - 1;

					offsets[digitInt + 1]++;
					digits[i] = digitInt;
					source[i] = (element, key);
				}

				for (var i = 2; i < offsets.Length; i++)
					offsets[i] += offsets[i - 1];

				for (var i = 0; i < source.Length; i++)
					temp[offsets[digits[i]]++] = source[i];

				temp.CopyTo(source);
			}
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

			var negative = LsdRadixSort.Sort(keyedArray, reverse);

			var range = reverse ? ^negative.. : ..negative;
			keyedArray.Elements.AsSpan(range).Reverse();
			return keyedArray;
		}
	}
}
