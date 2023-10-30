#pragma warning disable CS0660, CS0661, CA1067
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Algorithms.Sorting.Tests.TestUtilities;

/// <summary>
/// Custom floating point type for tests.
/// </summary>
/// <remarks>
/// The following properties are supported: <see cref="Zero"/>.
/// </remarks>
[SuppressMessage("ReSharper", "ArrangeMethodOrOperatorBody", Justification = "More compact.")]
public readonly struct CustomFloat : IFloatingPointIeee754<CustomFloat>
{
	/// <inheritdoc />
	public static CustomFloat Zero => default;

	/// <inheritdoc />
	public static CustomFloat One => throw new NotSupportedException();

	/// <inheritdoc />
	public static int Radix => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat AdditiveIdentity => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat MultiplicativeIdentity => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat E => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Pi => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Tau => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat NegativeOne => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Epsilon => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat NaN => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat NegativeInfinity => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat NegativeZero => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat PositiveInfinity => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator +(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool operator ==(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool operator !=(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool operator >(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool operator >=(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool operator <(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool operator <=(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator --(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator /(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator ++(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator *(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator -(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator -(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator +(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat operator %(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public int CompareTo(object? obj) => throw new NotSupportedException();

	/// <inheritdoc />
	public int CompareTo(CustomFloat other) => throw new NotSupportedException();

	/// <inheritdoc />
	public bool Equals(CustomFloat other) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Pow(CustomFloat x, CustomFloat y) => throw new NotSupportedException();

	/// <inheritdoc />
	public string ToString(string? format, IFormatProvider? formatProvider) => throw new NotSupportedException();

	/// <inheritdoc />
	public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Parse(string s, IFormatProvider? provider) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryParse(string? s, IFormatProvider? provider, out CustomFloat result) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Parse(ReadOnlySpan<char> s, IFormatProvider? provider) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryParse(ReadOnlySpan<char> s, IFormatProvider? provider, out CustomFloat result) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Abs(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsCanonical(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsComplexNumber(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsEvenInteger(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsFinite(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsImaginaryNumber(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsInfinity(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsInteger(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsNaN(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsNegative(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsNegativeInfinity(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsNormal(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsOddInteger(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsPositive(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsPositiveInfinity(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsRealNumber(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsSubnormal(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool IsZero(CustomFloat value) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat MaxMagnitude(CustomFloat x, CustomFloat y) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat MaxMagnitudeNumber(CustomFloat x, CustomFloat y) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat MinMagnitude(CustomFloat x, CustomFloat y) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat MinMagnitudeNumber(CustomFloat x, CustomFloat y) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Parse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Parse(string s, NumberStyles style, IFormatProvider? provider) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryConvertFromChecked<TOther>(TOther value, out CustomFloat result) where TOther : INumberBase<TOther> => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryConvertFromSaturating<TOther>(TOther value, out CustomFloat result) where TOther : INumberBase<TOther> => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryConvertFromTruncating<TOther>(TOther value, out CustomFloat result) where TOther : INumberBase<TOther> => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryConvertToChecked<TOther>(CustomFloat value, out TOther result) where TOther : INumberBase<TOther> => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryConvertToSaturating<TOther>(CustomFloat value, out TOther result) where TOther : INumberBase<TOther> => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryConvertToTruncating<TOther>(CustomFloat value, out TOther result) where TOther : INumberBase<TOther> => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, IFormatProvider? provider, out CustomFloat result) => throw new NotSupportedException();

	/// <inheritdoc />
	public static bool TryParse(string? s, NumberStyles style, IFormatProvider? provider, out CustomFloat result) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Exp(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Exp10(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Exp2(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public int GetExponentByteCount() => throw new NotSupportedException();

	/// <inheritdoc />
	public int GetExponentShortestBitLength() => throw new NotSupportedException();

	/// <inheritdoc />
	public int GetSignificandBitLength() => throw new NotSupportedException();

	/// <inheritdoc />
	public int GetSignificandByteCount() => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Round(CustomFloat x, int digits, MidpointRounding mode) => throw new NotSupportedException();

	/// <inheritdoc />
	public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten) => throw new NotSupportedException();

	/// <inheritdoc />
	public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten) => throw new NotSupportedException();

	/// <inheritdoc />
	public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten) => throw new NotSupportedException();

	/// <inheritdoc />
	public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Acosh(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Asinh(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Atanh(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Cosh(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Sinh(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Tanh(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Log(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Log(CustomFloat x, CustomFloat newBase) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Log10(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Log2(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Cbrt(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Hypot(CustomFloat x, CustomFloat y) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat RootN(CustomFloat x, int n) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Sqrt(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Acos(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat AcosPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Asin(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat AsinPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Atan(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat AtanPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Cos(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat CosPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Sin(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static (CustomFloat Sin, CustomFloat Cos) SinCos(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static (CustomFloat SinPi, CustomFloat CosPi) SinCosPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat SinPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Tan(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat TanPi(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Atan2(CustomFloat y, CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Atan2Pi(CustomFloat y, CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat BitDecrement(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat BitIncrement(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat FusedMultiplyAdd(CustomFloat left, CustomFloat right, CustomFloat addend) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat Ieee754Remainder(CustomFloat left, CustomFloat right) => throw new NotSupportedException();

	/// <inheritdoc />
	public static int ILogB(CustomFloat x) => throw new NotSupportedException();

	/// <inheritdoc />
	public static CustomFloat ScaleB(CustomFloat x, int n) => throw new NotSupportedException();
}
