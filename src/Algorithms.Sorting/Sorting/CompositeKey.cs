namespace Algorithms.Sorting.Sorting;

/// <summary>
/// Composite key of two types.
/// </summary>
/// <typeparam name="TPrimary">Primary key type.</typeparam>
/// <typeparam name="TSecondary">Secondary key type.</typeparam>
internal sealed class CompositeKey<TPrimary, TSecondary>
{
	private readonly TPrimary _primary;
	private readonly TSecondary _secondary;

	/// <summary>
	/// Initializes a new instance of the <see cref="CompositeKey{TPrimary, TSecondary}"/> class.
	/// </summary>
	/// <param name="primary">Primary key value.</param>
	/// <param name="secondary">Secondary key value.</param>
	public CompositeKey(TPrimary primary, TSecondary secondary)
	{
		_primary = primary;
		_secondary = secondary;
	}

	/// <summary>
	/// <see cref="CompositeKey{TPrimary,TSecondary}"/> comparer.
	/// </summary>
	public sealed class Comparer : Comparer<CompositeKey<TPrimary, TSecondary>>
	{
		private readonly IComparer<TPrimary> _primaryComparer;
		private readonly IComparer<TSecondary> _secondaryComparer;

		/// <summary>
		/// Initializes a new instance of the <see cref="Comparer"/> class.
		/// </summary>
		/// <param name="primaryComparer">Primary key comparer.</param>
		/// <param name="secondaryComparer">Secondary key comparer.</param>
		public Comparer(IComparer<TPrimary> primaryComparer, IComparer<TSecondary> secondaryComparer)
		{
			_primaryComparer = primaryComparer;
			_secondaryComparer = secondaryComparer;
		}

		/// <inheritdoc/>
		public override int Compare(CompositeKey<TPrimary, TSecondary>? x, CompositeKey<TPrimary, TSecondary>? y)
		{
			if (x is null)
				return y is null ? 0 : -1;

			if (y is null)
				return 1;

			if (_primaryComparer.Compare(x._primary, y._primary) is var primary and not 0)
				return primary;

			return _secondaryComparer.Compare(x._secondary, y._secondary);
		}
	}
}
