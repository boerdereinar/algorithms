using System.Diagnostics;
using Algorithms.Sorting.Algorithms.Impractical;
using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Tests.Algorithms.Impractical;

/// <summary>
/// Tests for <see cref="PermutationSort{TKey}"/>.
/// </summary>
public sealed class PermutationSortTests : SlowSortingAlgorithmTestBase
{
	/// <inheritdoc />
	protected override Type Type => typeof(PermutationSort<>);

	/// <summary>
	/// Tests if <see cref="PermutationSort{TKey}.OrderBy{TKey}"/> throws an <see cref="UnreachableException"/>.
	/// </summary>
	[Fact]
	public void OrderBy_IllegalComparer_ThrowsUnreachableException()
	{
		var source = new[] { 1, 2, 3 };
		var comparer = new IllegalComparer<int>();
		var algorithm = PermutationSort<int>.Default;

		Assert.Throws<UnreachableException>(() => source.OrderBy(x => x, comparer, algorithm).ToArray());
	}

	private sealed class IllegalComparer<T> : IComparer<T>
	{
		/// <inheritdoc />
		public int Compare(T? x, T? y)
		{
			return 1;
		}
	}
}
