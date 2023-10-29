using SortingAlgorithms.Algorithms.DistributionSorts;
using SortingAlgorithms.Tests.TestUtilities;

namespace SortingAlgorithms.Tests.Algorithms.DistributionSorts;

/// <summary>
/// Tests for <see cref="LsdRadixSort"/>.
/// </summary>
public static class LsdRadixSortTests
{
	/// <summary>
	/// Tests for <see cref="LsdRadixSort.Integer{TKey}"/>.
	/// </summary>
	public sealed class IntegerTests : SortingAlgorithmTestBase<IntegerTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(LsdRadixSort.Integer<>);

		/// <summary>
		/// Tests if the constructor throws an <see cref="ArgumentOutOfRangeException"/> when the radix is invalid.
		/// </summary>
		[Fact]
		public void Constructor_InvalidRadix_ThrowsArgumentOutOfRangeException()
		{
			var ex = Assert.Throws<ArgumentOutOfRangeException>("radix", () => new LsdRadixSort.Integer<int>(1));
			AssertException.MessageNotEmpty(ex);
		}
	}

	/// <summary>
	/// Tests for <see cref="LsdRadixSort.Integer{TKey}"/> with radix 10.
	/// </summary>
	public sealed class IntegerRadix10Tests : SortingAlgorithmTestBase<IntegerRadix10Tests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(LsdRadixSort.Integer<>);

		/// <inheritdoc />
		protected override object?[] Arguments => new object[] { 10 };
	}

	/// <summary>
	/// Tests for <see cref="LsdRadixSort.FloatingPoint{TKey}"/>.
	/// </summary>
	public sealed class FloatingPointTests : SortingAlgorithmTestBase<FloatingPointTests>, ISortingAlgorithmTest
	{
		/// <inheritdoc />
		protected override Type Type => typeof(LsdRadixSort.FloatingPoint<>);

		/// <summary>
		/// Tests if the sorting algorithm throws a <see cref="NotSupportedException"/> when sorting user-defined floats.
		/// </summary>
		[Fact]
		public void SortCustomFloat_ThrowsNotSupportedException()
		{
			var algorithm = LsdRadixSort.FloatingPoint<CustomFloat>.Default;
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
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		[SkippableTheory]
		[MemberData(nameof(Half), false)]
		[MemberData(nameof(Half), true)]
		public Task SortHalf_SortsCorrectly(IEnumerable<Half> source, IEnumerable<Half> expected, bool reverse)
		{
			return SortTestBase(source, expected, reverse);
		}

		/// <summary>
		/// Tests if the sorting algorithm sorts floats correctly.
		/// </summary>
		/// <param name="source">The source collection.</param>
		/// <param name="expected">The expected sorted collection.</param>
		/// <param name="reverse">Whether the data is reversed.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		[SkippableTheory]
		[MemberData(nameof(Float), false)]
		[MemberData(nameof(Float), true)]
		public Task SortFloat_SortsCorrectly(IEnumerable<float> source, IEnumerable<float> expected, bool reverse)
		{
			return SortTestBase(source, expected, reverse);
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
}