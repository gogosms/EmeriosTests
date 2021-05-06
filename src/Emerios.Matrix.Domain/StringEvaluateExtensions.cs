using System.Linq;

namespace Emerios.Matrix.Domain
{
	/// <summary>
	/// Class that returns the underlying characters that are repeated the most
	/// </summary>
	public static class StringEvaluateExtensions
	{
		public static string EvaluateLongestRun(this string rowAsString)
		{
			return new string(rowAsString.Select((c, index) => rowAsString.Substring(index).TakeWhile(e => e == c))
				.OrderByDescending(e => e.Count())
				.First().ToArray());
		}
	}
}