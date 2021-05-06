using Emerios.Matrix.Domain;
using NUnit.Framework;

namespace Emerios.Matrix.Tests
{
	[TestFixture]
	public class StringRepeatedTests
	{
		[TestCase("ABBBAAAACCCCCCCCCC", "CCCCCCCCCC")]
		[TestCase("AABBAAABBBDDDD", "DDDD")]
		[TestCase("AABBAAA7777777BBBDDDD", "7777777")]
		public void Must_Correctly_Find_Repeated_Character(string stringEvaluate, string expectedString)
		{
			var stringResult = stringEvaluate.EvaluateLongestRun();
			Assert.IsNotNull(stringResult);
			Assert.That(stringResult, Is.EqualTo(expectedString));
		}
	}
}