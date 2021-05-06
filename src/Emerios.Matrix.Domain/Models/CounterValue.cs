using System.Diagnostics;

namespace Emerios.Matrix.Domain.Models
{
	[DebuggerDisplay("{" + nameof(Values) + "}")]
	public class CounterValue
	{
		public string Values { get; set; }

		public int Count => Values.Length;

	}
}