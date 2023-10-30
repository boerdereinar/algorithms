namespace Algorithms.Sorting.Tests.TestUtilities;

/// <summary>
/// Exception assertion helper methods.
/// </summary>
public static class AssertException
{
	/// <summary>
	/// Tests if the exception contains a message.
	/// </summary>
	/// <param name="exception">The exception.</param>
	/// <typeparam name="T">The type of the exception.</typeparam>
	public static void MessageNotEmpty<T>(T exception) where T : ArgumentException
	{
		Assert.Matches(@"^.+ \(Parameter '[^']+'\)(?:\nActual value was .*\.$|$)", exception.Message);
	}
}
