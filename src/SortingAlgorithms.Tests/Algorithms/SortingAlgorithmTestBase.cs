using System.Reflection;
using SortingAlgorithms.Sorting;
using SortingAlgorithms.Tests.TestUtilities;
using SkipException = Xunit.SkipException;

namespace SortingAlgorithms.Tests.Algorithms;

/// <summary>
/// Base class for sorting algorithm tests.
/// </summary>
/// <typeparam name="TSelf">The type that implements the abstract class.</typeparam>
public abstract class SortingAlgorithmTestBase<TSelf>
	where TSelf : SortingAlgorithmTestBase<TSelf>, ISortingAlgorithmTest
{
	/// <summary>
	/// Gets the type of the sorting algorithm.
	/// </summary>
	protected abstract Type Type { get; }

	/// <summary>
	/// Gets the arguments for the constructor of the sorting algorithm.
	/// </summary>
	protected virtual object?[]? Arguments => null;

	/// <summary>
	/// Tests if the sorting algorithm sorts integers correctly.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="expected">The expected sorted collection.</param>
	/// <param name="reverse">Whether the data is reversed.</param>
	[SkippableTheory]
	[MemberData(nameof(IntegerData), false)]
	[MemberData(nameof(IntegerData), true)]
	public void SortInteger_SortsCorrectly(IEnumerable<int> source, IEnumerable<int> expected, bool reverse)
	{
		 SortTestBase(source, expected, reverse);
	}

	/// <summary>
	/// Tests if the sorting algorithm sorts integers correctly.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="expected">The expected sorted collection.</param>
	/// <param name="reverse">Whether the data is reversed.</param>
	[SkippableTheory]
	[MemberData(nameof(UnsignedIntegerData), false)]
	[MemberData(nameof(UnsignedIntegerData), true)]
	public void SortUnsigned_SortsCorrectly(IEnumerable<uint> source, IEnumerable<uint> expected, bool reverse)
	{
		 SortTestBase(source, expected, reverse);
	}

	/// <summary>
	/// Tests if the sorting algorithm sorts doubles correctly.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="expected">The expected sorted collection.</param>
	/// <param name="reverse">Whether the data is reversed.</param>
	[SkippableTheory]
	[MemberData(nameof(DoubleData), false)]
	[MemberData(nameof(DoubleData), true)]
	public void SortDouble_SortsCorrectly(IEnumerable<double> source, IEnumerable<double> expected, bool reverse)
	{
		 SortTestBase(source, expected, reverse);
	}

	/// <summary>
	/// Tests if the sorting algorithm sorts strings correctly.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="expected">The expected sorted collection.</param>
	/// <param name="reverse">Whether the data is reversed.</param>
	[SkippableTheory]
	[MemberData(nameof(StringData), false)]
	[MemberData(nameof(StringData), true)]
	public void SortString_SortsCorrectly(IEnumerable<string> source, IEnumerable<string> expected, bool reverse)
	{
		 SortTestBase(source, expected, reverse);
	}

	/// <summary>
	/// Tests if the sorting algorithm sorts composite data correctly.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="expected">The expected sorted collection.</param>
	/// <param name="reverse">Whether the data is reversed.</param>
	[SkippableTheory]
	[MemberData(nameof(CompositeData), false)]
	[MemberData(nameof(CompositeData), true)]
	public virtual void SortComposite_SortsCorrectly(IEnumerable<int> source, IEnumerable<int> expected, bool reverse)
	{
		var algorithm = TryCreateInstance<int>(Type, Arguments) ?? throw new SkipException("Unsupported type!");
		var comparer = reverse ? Comparer<int>.Default.Reverse() : Comparer<int>.Default;

		try
		{
			Assert.Equal(expected, source.OrderBy(x => x % 2, comparer, algorithm).ThenBy(x => x, comparer));
		}
		catch (NotSupportedException)
		{
			throw new SkipException("Unsupported type!");
		}
	}

	/// <summary>
	/// Tests if <see cref="ISortingAlgorithm{TKey}.CreateComposite{TKey}"/> either returns or throws a <see cref="NotSupportedException"/>.
	/// </summary>
	[Fact]
	public void CreateComposite_EitherReturnsOrThrowsNotSupportedException()
	{
		try
		{
#pragma warning disable xUnit2007
			if (TryCreateInstance<int>(Type, Arguments) is { } i)
				Assert.IsAssignableFrom(typeof(ISortingAlgorithm<int>), i.CreateComposite<int>());
			else if (TryCreateInstance<uint>(Type, Arguments) is { } u)
				Assert.IsAssignableFrom(typeof(ISortingAlgorithm<uint>), u.CreateComposite<uint>());
			else if (TryCreateInstance<double>(Type, Arguments) is { } d)
				Assert.IsAssignableFrom(typeof(ISortingAlgorithm<double>), d.CreateComposite<double>());
			else if (TryCreateInstance<string>(Type, Arguments) is { } s)
				Assert.IsAssignableFrom(typeof(ISortingAlgorithm<string>), s.CreateComposite<string>());
			else
				Assert.Fail("Unsupported type!");
#pragma warning restore xUnit2007
		}
		catch (NotSupportedException)
		{
			// ignore
		}
	}

	/// <summary>
	/// Tests if <see cref="ISortingAlgorithm{TKey}.Default"/> returns the correct instance.
	/// </summary>
	[Fact]
	public void Default_ReturnsDefaultInstance()
	{
		Type? type = null;
		foreach (var key in new[] { typeof(int), typeof(uint), typeof(double), typeof(string) })
		{
			try
			{
				type = Type.ContainsGenericParameters ? Type.MakeGenericType(key) : Type;
				break;
			}
			catch
			{
				// ignore
			}
		}

		Assert.NotNull(type);

		var instance = type.GetProperty(nameof(ISortingAlgorithm<int>.Default), BindingFlags.Public | BindingFlags.Static)?.GetValue(null, null);
		Assert.NotNull(instance);
		Assert.Equal(type, instance.GetType());
	}

	/// <inheritdoc cref="ISortingAlgorithmTest.Integer"/>
	public static SortTheoryData<int> IntegerData(bool reverse)
	{
		return TSelf.Integer(reverse);
	}

	/// <inheritdoc cref="ISortingAlgorithmTest.UnsignedInteger"/>
	public static SortTheoryData<uint> UnsignedIntegerData(bool reverse)
	{
		return TSelf.UnsignedInteger(reverse);
	}

	/// <inheritdoc cref="ISortingAlgorithmTest.Double"/>
	public static SortTheoryData<double> DoubleData(bool reverse)
	{
		return TSelf.Double(reverse);
	}

	/// <inheritdoc cref="ISortingAlgorithmTest.String"/>
	public static SortTheoryData<string> StringData(bool reverse)
	{
		return TSelf.String(reverse);
	}

	/// <inheritdoc cref="ISortingAlgorithmTest.Composite"/>
	public static SortTheoryData<int> CompositeData(bool reverse)
	{
		return TSelf.Composite(reverse);
	}

	/// <summary>
	/// Tests if the sorting algorithm given by <see cref="Type"/> sorts the data correctly.
	/// </summary>
	/// <param name="source">The source collection.</param>
	/// <param name="expected">The expected sorted collection.</param>
	/// <param name="reverse">Whether the data is reversed.</param>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	protected virtual void SortTestBase<TKey>(IEnumerable<TKey> source, IEnumerable<TKey> expected, bool reverse)
	{
		var algorithm = TryCreateInstance<TKey>(Type, Arguments) ?? throw new SkipException("Unsupported type!");
		var comparer = reverse ? Comparer<TKey>.Default.Reverse() : Comparer<TKey>.Default;

		Assert.Equal(expected, source.Order(comparer, algorithm));
	}

	private static ISortingAlgorithm<TKey>? TryCreateInstance<TKey>(Type type, object?[]? args)
	{
		try
		{
			if (type.ContainsGenericParameters)
				type = type.MakeGenericType(typeof(TKey));

			return Activator.CreateInstance(type, args) as ISortingAlgorithm<TKey>;
		}
		catch
		{
			return null;
		}
	}
}
