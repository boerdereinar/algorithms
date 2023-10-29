using System.Diagnostics;
using Xunit.Sdk;

namespace SortingAlgorithms.Tests.TestUtilities;

/// <summary>
/// Asynchronous assertion helper methods.
/// </summary>
public static class AssertAsync
{
	/// <summary>
	/// Tests if the action completes within the given timeout.
	/// </summary>
	/// <param name="action">Action.</param>
	/// <param name="timeout">Timeout.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	/// <exception cref="TestTimeoutException">If the action does not complete within the given timeout.</exception>
	/// <remarks>The timeout will be disabled if <see cref="Debugger.IsAttached"/> is <c>true</c>.</remarks>
	public static async Task CompletesIn(Action action, TimeSpan timeout)
	{
		var t = Task.Run(action);
		try
		{
			if (Debugger.IsAttached)
				await t.WaitAsync(CancellationToken.None);
			else
				await t.WaitAsync(timeout);
		}
		catch (TimeoutException)
		{
			throw new TestTimeoutException((int)timeout.TotalMilliseconds);
		}
	}
}
