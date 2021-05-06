using System.Collections.Generic;
using System.Linq;

namespace Emerios.Matrix.Domain.Models
{
	public class CounterValues
	{
		public CounterValues(IEnumerable<CounterValue> values)
		{
			Values = values;
		}

		public IEnumerable<CounterValue> Values { get; set; }

		public string MaxCharacter => Values.FirstOrDefault(p => p.Count == Values.Max(m => m.Count))?.Values;

	}
}