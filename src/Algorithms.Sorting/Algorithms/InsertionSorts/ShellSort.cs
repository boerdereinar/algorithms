using Algorithms.Sorting.Sorting;

namespace Algorithms.Sorting.Algorithms.InsertionSorts;

/// <summary>
/// Represents the <a href="https://en.wikipedia.org/wiki/Shellsort">Shellsort</a> sorting algorithm.
/// </summary>
/// <typeparam name="TKey">The type of elements to be sorted.</typeparam>
public sealed class ShellSort<TKey> : ISortingAlgorithm<TKey>
{
	private readonly ShellSortGapSequence _gapSequence;

	/// <summary>
	/// Initializes a new instance of the <see cref="ShellSort{TKey}"/> class.
	/// </summary>
	public ShellSort() : this(ShellSortGapSequence.Ciura) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="ShellSort{TKey}"/> class.
	/// </summary>
	/// <param name="gapSequence">Gap sequence.</param>
	public ShellSort(ShellSortGapSequence gapSequence)
	{
		_gapSequence = gapSequence;
	}

	/// <inheritdoc />
	public static ISortingAlgorithm<TKey> Default { get; } = new ShellSort<TKey>();

	/// <inheritdoc />
	public IEnumerable<TSource> OrderBy<TSource>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
	{
		var keyedArray = source.ToKeyedArray(keySelector, comparer);
		foreach (var gap in _gapSequence.GapSequence(keyedArray.Length))
		{
			for (var i = gap; i < keyedArray.Length; i++)
			{
				var value = keyedArray[i];
				var j = i;
				for (; j >= gap && keyedArray.Compare(j - gap, value.Key) > 0; j -= gap)
					keyedArray[j] = keyedArray[j - gap];

				keyedArray[j] = value;
			}
		}

		return keyedArray;
	}

	/// <inheritdoc />
	public ISortingAlgorithm<T> CreateComposite<T>()
	{
		return new ShellSort<T>();
	}
}

/// <summary>
/// Gap sequence for <see cref="ShellSort{TKey}"/>.
/// </summary>
public abstract class ShellSortGapSequence
{
	/// <inheritdoc cref="ShellGapSequence"/>
	public static ShellSortGapSequence Shell => new ShellGapSequence();

	/// <inheritdoc cref="FrankLazarusGapSequence"/>
	public static ShellSortGapSequence FrankLazarus => new FrankLazarusGapSequence();

	/// <inheritdoc cref="HibbardGapSequence"/>.
	public static ShellSortGapSequence Hibbard => new HibbardGapSequence();

	/// <inheritdoc cref="PapernovStasevichGapSequence"/>
	public static ShellSortGapSequence PapernovStasevich => new PapernovStasevichGapSequence();

	/// <inheritdoc cref="PrattGapSequence"/>
	public static ShellSortGapSequence Pratt => new PrattGapSequence();

	/// <inheritdoc cref="KnuthGapSequence"/>
	public static ShellSortGapSequence Knuth => new KnuthGapSequence();

	/// <inheritdoc cref="IncerpiSedgewickGapSequence"/>
	public static ShellSortGapSequence IncerpiSedgewick => new IncerpiSedgewickGapSequence();

	/// <inheritdoc cref="Sedgewick1982GapSequence"/>
	public static ShellSortGapSequence Sedgewick1982 => new Sedgewick1982GapSequence();

	/// <inheritdoc cref="Sedgewick1986GapSequence"/>
	public static ShellSortGapSequence Sedgewick1986 => new Sedgewick1986GapSequence();

	/// <inheritdoc cref="GonnetBaezaYatesGapSequence"/>
	public static ShellSortGapSequence GonnetBaezaYates => new GonnetBaezaYatesGapSequence();

	/// <inheritdoc cref="TokudaGapSequence"/>
	public static ShellSortGapSequence Tokuda => new TokudaGapSequence();

	/// <inheritdoc cref="CiuraGapSequence"/>
	public static ShellSortGapSequence Ciura => new CiuraGapSequence();

	/// <inheritdoc cref="LeeGapSequence"/>
	public static ShellSortGapSequence Lee => new LeeGapSequence();

	/// <summary>
	/// Generates a gap sequence for <see cref="ShellSort{TKey}"/> in descending order.
	/// </summary>
	/// <param name="n">Maximum gap size.</param>
	/// <returns>Generated gap sequence.</returns>
	public abstract IEnumerable<int> GapSequence(int n);

	/// <summary>
	/// Shell gap sequence. Original shell sort gap sequence. $\Theta\left(N^2\right)$.
	/// </summary>
	/// <remarks>
	/// Shell, D. L. (1959). <a href="https://doi.org/10.1145%2F368370.368387">"A High-Speed Sorting Procedure"</a>. Communications of the ACM. 2 (7): 30–32. doi:10.1145/368370.368387. S2CID 28572656.
	/// </remarks>
	private sealed class ShellGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			for (var gap = n >> 1; gap > 0; gap >>= 1)
				yield return gap;
		}
	}

	/// <summary>
	/// Frank &amp; Lazarus gap sequence. $\Theta\left(N^\frac{3}{2}\right)$.
	/// </summary>
	/// <remarks>
	/// Frank, R. M.; Lazarus, R. B. (1960). <a href="https://doi.org/10.1145%2F366947.366957">"A High-Speed Sorting Procedure"</a>. Communications of the ACM. 3 (1): 20–22. doi:10.1145/366947.366957. S2CID 34066017.
	/// </remarks>
	private sealed class FrankLazarusGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			for (var k = n >> 2; ; k >>= 1)
			{
				yield return (k << 1) + 1;
				if (k == 0) break;
			}
		}
	}

	/// <summary>
	/// Hibbard gap sequence. $\Theta\left(N^\frac{3}{2}\right)$.
	/// </summary>
	/// <remarks>
	/// Hibbard, Thomas N. (1963). <a href="https://doi.org/10.1145%2F366552.366557">"An Empirical Study of Minimal Storage Sorting"</a>. Communications of the ACM. 6 (5): 206–213. doi:10.1145/366552.366557. S2CID 12146844.<br/>
	/// OEIS: <a href="https://oeis.org/A000225">A000225</a>.
	/// </remarks>
	private sealed class HibbardGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			for (var gap = 1 << int.Log2(n); gap > 1; gap >>= 1)
				yield return gap - 1;
		}
	}

	/// <summary>
	/// Papernov &amp; Stasevich gap sequence. $\Theta\left(N^\frac{3}{2}\right)$.
	/// </summary>
	/// <remarks>
	/// Papernov, A. A.; Stasevich, G. V. (1965). <a href="http://www.mathnet.ru/links/83f0a81df1ec06f76d3683c6cab7d143/ppi751.pdf">"A Method of Information Sorting in Computer Memories"</a>. Problems of Information Transmission. 1 (3): 63–75.<br/>
	/// OEIS: <a href="https://oeis.org/A083318">A083318</a>.
	/// </remarks>
	private sealed class PapernovStasevichGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			for (var gap = 1 << int.Log2(n - 2); gap > 1; gap >>= 1)
				yield return gap + 1;

			yield return 1;
		}
	}

	/// <summary>
	/// Pratt gap sequence. $\Theta\left(N\log^2 N\right)$.
	/// </summary>
	/// <remarks>
	/// Pratt, Vaughan Ronald (1979). <a href="https://apps.dtic.mil/sti/pdfs/AD0740110.pdf">Shellsort and Sorting Networks (Outstanding Dissertations in the Computer Sciences)</a>. Garland. ISBN 978-0-8240-4406-0.<br/>
	/// OEIS: <a href="https://oeis.org/A003586">A003586</a>.
	/// </remarks>
	private sealed class PrattGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			var smooth = new List<int> { 1 };
			var i2 = 0;
			var i3 = 0;
			while (true)
			{
				var n2 = 2 * smooth[i2];
				var n3 = 3 * smooth[i3];

				var min = Math.Min(n2, n3);
				if (min >= n) break;
				smooth.Add(Math.Min(n2, n3));

				i2 += n2 <= n3 ? 1 : 0;
				i3 += n2 >= n3 ? 1 : 0;
			}

			return Enumerable.Reverse(smooth);
		}
	}

	/// <summary>
	/// Knuth gap sequence.
	/// </summary>
	/// <remarks>
	/// Knuth, Donald E. (1997). "Shell's method". The Art of Computer Programming. Volume 3: Sorting and Searching (2nd ed.). Reading, Massachusetts: Addison-Wesley. pp. 83–95. ISBN 978-0-201-89685-5.<br/>
	/// OEIS: <a href="https://oeis.org/A003462">A003462</a>.
	/// </remarks>
	private sealed class KnuthGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			var max = (int)Math.Ceiling(n / 3d) + 1;
			var k = (int)Math.Pow(3, (int)Math.Log(max * 2 + 1, 3));
			for (; k > 1; k /= 3)
				yield return k - 1 >> 1;
		}
	}

	/// <summary>
	/// Incerpi &amp; Sedgewick gap sequence. $\mathcal O\left(N^{1 + \sqrt{\frac{8ln(5/2)}{\ln(N)}}}\right)$.
	/// </summary>
	/// <remarks>
	/// Incerpi, Janet; Sedgewick, Robert (1985). <a href="https://hal.inria.fr/inria-00076291/file/RR-0267.pdf">"Improved Upper Bounds on Shellsort"</a>. Journal of Computer and System Sciences. 31 (2): 210–224. doi:10.1016/0022-0000(85)90042-x.<br/>
	/// OEIS: <a href="https://oeis.org/A036569">A036569</a>.
	/// </remarks>
	private sealed class IncerpiSedgewickGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			return new[] { 2085837936, 852913488, 343669872, 114556624, 49095696, 21479367, 8382192, 3402672, 1391376, 463792, 198768, 86961, 33936, 13776, 4592, 1968, 861, 336, 112, 48, 21, 7, 3, 1, }.SkipWhile(gap => gap >= n);
		}
	}

	/// <summary>
	/// Sedgewick 1982 gap sequence. $\mathcal O\left(N^\frac{4}{3}\right)$.
	/// </summary>
	/// <remarks>
	/// Sedgewick, Robert (1998). Algorithms in C. Vol. 1 (3rd ed.). Addison-Wesley. pp. 273–281. ISBN 978-0-201-31452-6.<br/>
	/// OEIS: <a href="https://oeis.org/A036562">A036562</a>.
	/// </remarks>
	private sealed class Sedgewick1982GapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			return Generate().TakeWhile(gap => gap < n).Reverse();
		}

		private static IEnumerable<int> Generate()
		{
			var k4 = 4;
			var k2 = 1;

			yield return 1;
			for (; ; k4 <<= 2, k2 <<= 1)
				yield return k4 + 3 * k2 + 1;
		}
	}

	/// <summary>
	/// Sedgewick 1986 gap sequence. $\mathcal O\left(N^\frac{4}{3}\right)$.
	/// </summary>
	/// <remarks>
	/// Sedgewick, Robert (1986). <a href="https://doi.org/10.1016%2F0196-6774%2886%2990001-5">"A New Upper Bound for Shellsort"</a>. Journal of Algorithms. 7 (2): 159–173. doi:10.1016/0196-6774(86)90001-5.<br/>
	/// OEIS: <a href="https://oeis.org/A033622">A033622</a>.
	/// </remarks>
	private sealed class Sedgewick1986GapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			return Generate().TakeWhile(gap => gap < n).Reverse();
		}

		private static IEnumerable<int> Generate()
		{
			var even = true;
			for (int k2 = 1, kHalf = 1; ; k2 <<= 1, even = !even)
			{
				yield return even ? 9 * (k2 - kHalf) + 1 : 8 * k2 - 6 * kHalf + 1;
				if (even)
					kHalf <<= 1;
			}
		}
	}

	/// <summary>
	/// Gonnet &amp; Baeza-Yates gap sequence.
	/// </summary>
	/// <remarks>
	/// Gonnet, Gaston H.; Baeza-Yates, Ricardo (1991). "Shellsort". Handbook of Algorithms and Data Structures: In Pascal and C (2nd ed.). Reading, Massachusetts: Addison-Wesley. pp. 161–163. ISBN 978-0-201-41607-7.
	/// </remarks>
	private sealed class GonnetBaezaYatesGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			for (var gap = n; gap > 1;)
				yield return gap = Math.Max((5 * gap - 1) / 11, 1);
		}
	}

	/// <summary>
	/// Tokuda gap sequence.
	/// </summary>
	/// <remarks>
	/// Tokuda, Naoyuki (1992). "An Improved Shellsort". In van Leeuven, Jan (ed.). Proceedings of the IFIP 12th World Computer Congress on Algorithms, Software, Architecture. Amsterdam: North-Holland Publishing Co. pp. 449–457. ISBN 978-0-444-89747-3.<br/>
	/// OEIS: <a href="https://oeis.org/A108870">A108870</a>.
	/// </remarks>
	private sealed class TokudaGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			return Generate().TakeWhile(gap => gap < n).Reverse();
		}

		private static IEnumerable<int> Generate()
		{
			for (var k = 2.25; ; k *= 2.25)
				yield return (int)Math.Ceiling((k - 1) / 1.25);
		}
	}

	/// <summary>
	/// Ciura gap sequence.
	/// </summary>
	/// <remarks>
	/// Ciura, Marcin (2001). <a href="https://web.archive.org/web/20180923235211/http://sun.aei.polsl.pl/~mciura/publikacje/shellsort.pdf">"Best Increments for the Average Case of Shellsort"</a>. In Freiwalds, Rusins (ed.). Proceedings of the 13th International Symposium on Fundamentals of Computation Theory. London: Springer-Verlag. pp. 106–117. ISBN 978-3-540-42487-1.<br/>
	/// OEIS: <a href="https://oeis.org/A102549">A102549</a>.
	/// </remarks>
	private sealed class CiuraGapSequence : ShellSortGapSequence
	{
		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			return new[] { 1750, 701, 301, 132, 57, 23, 10, 4, 1 }.SkipWhile(gap => gap >= n);
		}
	}

	/// <summary>
	/// Lee gap sequence.
	/// </summary>
	/// <remarks>
	/// Lee, Ying Wai (2021). <a href="https://arxiv.org/abs/2112.11112">"Empirically Improved Tokuda Gap Sequence in Shellsort"</a>. arXiv. doi:10.48550/ARXIV.2112.11112.
	/// </remarks>
	private sealed class LeeGapSequence : ShellSortGapSequence
	{
		private const double Gamma = 2.243609061420001;

		/// <inheritdoc />
		public override IEnumerable<int> GapSequence(int n)
		{
			var pow = 1d;
			return Enumerable.Range(1, int.MaxValue)
				.Select(_ => (int)Math.Ceiling(((pow *= Gamma) - 1) / (Gamma - 1)))
				.TakeWhile(gap => gap < n)
				.Reverse();
		}
	}
}
