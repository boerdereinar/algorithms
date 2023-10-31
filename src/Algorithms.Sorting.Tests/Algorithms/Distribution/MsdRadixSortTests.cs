using Algorithms.Common.Tests.TestUtilities;
using Algorithms.Sorting.Algorithms.Distribution;
using Algorithms.Sorting.Tests.TestUtilities;

namespace Algorithms.Sorting.Tests.Algorithms.Distribution;

/// <summary>
/// Tests for <see cref="MsdRadixSort"/>.
/// </summary>
public static class MsdRadixSortTests
{
	/// <summary>
	/// Tests for <see cref="MsdRadixSort.Integer{TKey}"/>.
	/// </summary>
	public sealed class Integer : SortingAlgorithmTestBase<Integer>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(MsdRadixSort.Integer<>);

		/// <summary>
		/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> when the radix is invalid.
		/// </summary>
		[Fact]
		public void Constructor_InvalidRadix_ThrowsArgumentOutOfRangeException()
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>("radix", () => new MsdRadixSort.Integer<int>(1));
			AssertException.MessageNotEmpty(ex);
		}
	}

	/// <summary>
	/// Tests for <see cref="LsdRadixSort.Integer{TKey}"/> with radix 10.
	/// </summary>
	public sealed class IntegerRadix10Tests : SortingAlgorithmTestBase<IntegerRadix10Tests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(MsdRadixSort.Integer<>);

		/// <inheritdoc />
		protected override object?[] Arguments => new object[] { 10 };
	}

	/// <summary>
	/// Tests for <see cref="MsdRadixSort.FloatingPoint{TKey}"/>.
	/// </summary>
	public sealed class FloatingPoint : SortingAlgorithmTestBase<FloatingPoint>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(MsdRadixSort.FloatingPoint<>);

		/// <summary>
		/// Tests if the sorting algorithm throws a <see cref="NotSupportedException"/> when sorting user-defined floats.
		/// </summary>
		[Fact]
		public void SortCustomFloat_ThrowsNotSupportedException()
		{
			var algorithm = MsdRadixSort.FloatingPoint<CustomFloat>.Default;
			var source = Enumerable.Empty<CustomFloat>();
			var comparer = Comparer<CustomFloat>.Default;

			Assert.Throws<NotSupportedException>(() => algorithm.OrderBy(source, x => x, comparer));
		}

		/// <summary>
		/// Tests if the sorting algorithm sorts halfs correctly.
		/// </summary>
		/// <param name="source">The source collection.</param>
		/// <param name="expected">The expected sorted collection.</param>
		/// <param name="reverse">Whether the data is reversed.</param>
		[SkippableTheory]
		[MemberData(nameof(Half), false)]
		[MemberData(nameof(Half), true)]
		public void SortHalf_SortsCorrectly(IEnumerable<Half> source, IEnumerable<Half> expected, bool reverse)
		{
			SortTestBase(source, expected, reverse);
		}

		/// <summary>
		/// Tests if the sorting algorithm sorts floats correctly.
		/// </summary>
		/// <param name="source">The source collection.</param>
		/// <param name="expected">The expected sorted collection.</param>
		/// <param name="reverse">Whether the data is reversed.</param>
		[SkippableTheory]
		[MemberData(nameof(Float), false)]
		[MemberData(nameof(Float), true)]
		public void SortFloat_SortsCorrectly(IEnumerable<float> source, IEnumerable<float> expected, bool reverse)
		{
			SortTestBase(source, expected, reverse);
		}

		/// <summary>
		/// Gets the data for sorting halfs.
		/// </summary>
		/// <param name="reverse">Whether the data is reversed.</param>
		/// <returns>The data for sorting halfs.</returns>
		public static SortTheoryData<Half> Half(bool reverse)
		{
			return SortTheoryDataExtensions.CreateNumeric<Half>(reverse);
		}

		/// <summary>
		/// Gets the data for sorting floats.
		/// </summary>
		/// <param name="reverse">Whether the data is reversed.</param>
		/// <returns>The data for sorting floats.</returns>
		public static SortTheoryData<float> Float(bool reverse)
		{
			return SortTheoryDataExtensions.CreateNumeric<float>(reverse);
		}
	}

	/// <summary>
	/// Tests for <see cref="MsdRadixSort.String"/>.
	/// </summary>
	public sealed class String : SortingAlgorithmTestBase<String>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(MsdRadixSort.String);

		/// <summary>
		/// Tests if <see cref="MsdRadixSort.String.OrderBy{TSource}"/> throws an <see cref="ArgumentOutOfRangeException"/> if
		/// the source contains non-ASCII characters.
		/// </summary>
		[Fact]
		public void OrderBy_NonAscii_ThrowsArgumentOutOfRangeException()
		{
			var source = new[] { "ඞ", "ಠ_ಠ" };
			var ex = Assert.Throws<ArgumentOutOfRangeException>("source", () => MsdRadixSort.String.Default.OrderBy(source, x => x, Comparer<string>.Default));
			Assert.Equal('ඞ', ex.ActualValue);
			AssertException.MessageNotEmpty(ex);
		}
	}
}
