using System.Numerics;
using Algorithms.Common.Utilities;

namespace Algorithms.Sorting.Tests.TestUtilities;

/// <summary>
/// Represents a set of data for a theory for a sorting test. Data can be added to the data set using the collection
/// initializer syntax.
/// </summary>
/// <typeparam name="T">The type of the data.</typeparam>
public sealed class SortTheoryData<T> : TheoryData<IEnumerable<T>, IEnumerable<T>, bool>
{
	private readonly Random _random = new(42);
	private readonly bool _reverse;

	/// <summary>
	/// Initializes a new instance of the <see cref="SortTheoryData{T}"/> class.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	public SortTheoryData(bool reverse = false)
	{
		_reverse = reverse;
	}

	/// <summary>
	/// Adds data to the theory data set.
	/// </summary>
	/// <param name="unsorted">The unsorted data value.</param>
	public void Add(IEnumerable<T> unsorted)
	{
		var source = unsorted.ToArray();
		var expected = _reverse ? source.OrderDescending() : source.Order();

		AddRow(source, expected.ToArray(), _reverse);
	}

	/// <summary>
	/// Adds data to the theory data set.
	/// </summary>
	/// <param name="unsorted">The unsorted data value.</param>
	/// <param name="sorted">The sorted data value.</param>
	public void Add(IEnumerable<T> unsorted, IEnumerable<T> sorted)
	{
		var expected = _reverse ? sorted.Reverse() : sorted;

		AddRow(unsorted.ToArray(), expected.ToArray(), _reverse);
	}

	/// <summary>
	/// Adds data to the theory data set.
	/// </summary>
	/// <param name="sorted">The sorted data value.</param>
	/// <param name="shuffle">Whether to shuffle the data.</param>
	public void Add(IEnumerable<T> sorted, bool shuffle)
	{
		var expected = (_reverse ? sorted.Reverse() : sorted).ToArray();
		var source = (shuffle ? _random.Shuffle(expected) : expected).ToArray();

		AddRow(source, expected.ToArray(), _reverse);
	}
}

/// <summary>
/// Extension methods for <see cref="SortTheoryData{T}"/>.
/// </summary>
public static class SortTheoryDataExtensions
{
	/// <summary>
	/// Creates a <see cref="SortTheoryData{T}"/> for numbers.
	/// </summary>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <param name="limits">Whether to include the minimum and maximum value of the values.</param>
	/// <typeparam name="TNumeric">The type of the number.</typeparam>
	/// <returns>The created <see cref="SortTheoryData{T}"/>.</returns>
	public static SortTheoryData<TNumeric> CreateNumeric<TNumeric>(bool reverse = false, bool limits = true)
		where TNumeric : INumber<TNumeric>, IMinMaxValue<TNumeric>
	{
		var data = new SortTheoryData<TNumeric>(reverse)
		{
			Enumerable.Empty<TNumeric>(),
			Enumerable.Range(0, 128),
			{ Enumerable.Range(0, 128), true },
			{ Enumerable.Range(0, 128).Select(x => x / 2), true },
		};

		if (limits)
		{
			var min = TNumeric.MinValue;
			var max = TNumeric.MaxValue;
			var mid = min / TNumeric.CreateChecked(2) + max / TNumeric.CreateChecked(2);
			data.Add(new[] { min, min + TNumeric.One, mid - TNumeric.One, mid, mid + TNumeric.One, max - TNumeric.One, max }, true);
		}

		if (typeof(TNumeric).GetInterface(typeof(ISignedNumber<>).Name) is not null)
		{
			data.Add(Enumerable.Range(-128, 128), true);
			data.Add(Enumerable.Range(-128, 256), true);
		}

		if (typeof(TNumeric).GetInterface(typeof(IFloatingPointIeee754<>).Name) is not null)
		{
			data.Add(Enumerable.Range(0, 128).Select(x => x / 16d), true);
			data.Add(Enumerable.Range(-128, 256).Select(x => x / 16d), true);

			if (limits)
				data.Add(new[] { double.NaN, double.NegativeInfinity, -1, 0, 1, double.PositiveInfinity }, true);
		}

		return data;
	}

	/// <summary>
	/// Adds and casts data to the theory data set.
	/// </summary>
	/// <param name="sortTheoryData">The theory data set.</param>
	/// <param name="unsorted">The unsorted data value.</param>
	/// <typeparam name="TSource">The type to cast to.</typeparam>
	/// <typeparam name="TAdd">The type to cast from.</typeparam>
	public static void Add<TSource, TAdd>(this SortTheoryData<TSource> sortTheoryData, IEnumerable<TAdd> unsorted)
		where TSource : INumber<TSource>
		where TAdd : INumber<TAdd>
	{
		sortTheoryData.Add(unsorted.Select(TSource.CreateChecked));
	}

	/// <summary>
	/// Adds and casts data to the theory data set.
	/// </summary>
	/// <param name="sortTheoryData">The theory data set.</param>
	/// <param name="sorted">The sorted data value.</param>
	/// <param name="shuffle">
	/// Whether to shuffle the data.
	/// </param>
	/// <typeparam name="TSource">The type to cast to.</typeparam>
	/// <typeparam name="TAdd">The type to cast from.</typeparam>
	public static void Add<TSource, TAdd>(this SortTheoryData<TSource> sortTheoryData, IEnumerable<TAdd> sorted, bool shuffle)
		where TSource : INumber<TSource>
		where TAdd : INumber<TAdd>
	{
		sortTheoryData.Add(sorted.Select(TSource.CreateChecked), shuffle);
	}

	/// <summary>
	/// Adds and casts data to the theory data set.
	/// </summary>
	/// <param name="sortTheoryData">The theory data set.</param>
	/// <param name="unsorted">The first data value.</param>
	/// <param name="sorted">The second data value.</param>
	/// <typeparam name="TSource">The type to cast to.</typeparam>
	/// <typeparam name="TAdd">The type to cast from.</typeparam>
	public static void Add<TSource, TAdd>(this SortTheoryData<TSource> sortTheoryData, IEnumerable<TAdd> unsorted, IEnumerable<TAdd> sorted)
		where TSource : INumber<TSource>
		where TAdd : INumber<TAdd>
	{
		sortTheoryData.Add(unsorted.Select(TSource.CreateChecked), sorted.Select(TSource.CreateChecked));
	}
}
