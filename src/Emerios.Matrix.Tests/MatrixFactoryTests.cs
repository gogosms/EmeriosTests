using System;
using System.Linq;
using Emerios.Matrix.Domain;
using Emerios.Matrix.Domain.Exceptions;
using NUnit.Framework;

namespace Emerios.Matrix.Tests
{
	[TestFixture]
	public class MatrixFactoryTests
	{
		private string _firstHorizontalRow;
		private string _secondHorizontalRow;
		private string _firstVerticalRow;
		private string _secondVerticalRow;
		private string[] _data;

		[SetUp]
		public void SetUp()
		{
			_firstHorizontalRow = "BBDADEF";
			_secondHorizontalRow = "BXCDDJK";
			_firstVerticalRow = "BBHRWAB";
			_secondVerticalRow = "BXY7N9X";
			_data = new[] { _firstHorizontalRow, _secondHorizontalRow, "HYI3DD3", "R7OÑGD2", "WNSPE0D", "A9CDDEF", "BXDDDJK" };
		}


		[TestCase(3, 3, true)]
		[TestCase(1, 2, false)]
		[TestCase(2, 2, true)]
		[TestCase(7, 7, true)]
		[TestCase(5, 2, false)]
		public void Must_Evaluate_If_Matrix_Is_Square(int width, int height, bool isValid)
		{
			if (isValid)
			{
				Assert.DoesNotThrow(() => MatrixExtensions.CreateMatrix(width, height, Array.Empty<string>()));
			}
			else
			{
				var expectedException = Assert.Throws<MatrixSquareException>(() => MatrixExtensions.CreateMatrix(width, height));
				Assert.IsNotNull(expectedException);
				Assert.That(expectedException.Message, Is.EqualTo($"It is not a square matrix. The Height: {height} must equal to Width: {width}."));
			}

		}

		[TestCase(1, 1, false)]
		[TestCase(2, 2, true)]
		[TestCase(3, 3, true)]
		public void Must_Evaluate_Correctly_Size(int width, int height, bool isValid)
		{
			if (isValid)
			{
				Assert.DoesNotThrow(() => MatrixExtensions.CreateMatrix(width, height, Array.Empty<string>()));
			}
			else
			{
				var expectedException = Assert.Throws<MatrixInvalidSizeException>(() => MatrixExtensions.CreateMatrix(width, height));
				Assert.IsNotNull(expectedException);
				Assert.That(expectedException.Message, Is.EqualTo($"The Height:{height} and Width: {width} must be greater than 1."));
			}
		}

		[Test]
		public void Must_Correctly_Get_Horizontal_Row()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var expectedHorizontalRow = expectedMatrix.GetHorizontalOrVertical(0);
			Assert.That(expectedHorizontalRow, Is.EqualTo(_firstHorizontalRow));
			expectedHorizontalRow = expectedMatrix.GetHorizontalOrVertical(1);
			Assert.That(expectedHorizontalRow, Is.EqualTo(_secondHorizontalRow));
		}

		[Test]
		public void Must_Correctly_Get_Vertical_Row()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var expectedVerticalRow = expectedMatrix.GetHorizontalOrVertical(0, false);
			Assert.That(expectedVerticalRow, Is.EqualTo(_firstVerticalRow));
			expectedVerticalRow = expectedMatrix.GetHorizontalOrVertical(1, false);
			Assert.That(expectedVerticalRow, Is.EqualTo(_secondVerticalRow));
		}

		[Test]
		public void Must_Correctly_Evaluate_Horizontal_Row()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var rowAsString = expectedMatrix.GetHorizontalOrVertical(0);
			var expectedCounter = rowAsString.EvaluateString();
			Assert.That(expectedCounter.Values, Is.EqualTo("BB"));
			Assert.That(expectedCounter.Count, Is.EqualTo(2));
		}

		[Test]
		public void Must_Correctly_Evaluate_Vertical_Row()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var rowAsString = expectedMatrix.GetHorizontalOrVertical(4, false);
			var expectedCounter = rowAsString.EvaluateString();
			Assert.That(expectedCounter.Values, Is.EqualTo("DDD"));
			Assert.That(expectedCounter.Count, Is.EqualTo(3));
		}

		[Test]
		public void Must_Correctly_Evaluate_Horizontal_Rows()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var horizontalRows = MatrixExtensions.EvaluateRows(i => expectedMatrix.GetHorizontalOrVertical(i));
			Assert.That(horizontalRows.MaxCharacter, Is.EqualTo("DDD"));
			var expectedCounter = horizontalRows.Values.ToList();
			Assert.That(expectedCounter.Count, Is.EqualTo(5));
			Assert.That(expectedCounter[0].Values, Is.EqualTo("BB"));
			Assert.That(expectedCounter[1].Values, Is.EqualTo("DD"));
			Assert.That(expectedCounter[2].Values, Is.EqualTo("DD"));
			Assert.That(expectedCounter[3].Values, Is.EqualTo("DD"));
			Assert.That(expectedCounter[4].Values, Is.EqualTo("DDD"));

		}

		[Test]
		public void Must_Correctly_Evaluate_Vertical_Rows()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var verticalRows = MatrixExtensions.EvaluateRows(i => expectedMatrix.GetHorizontalOrVertical(i, false));
			Assert.That(verticalRows.MaxCharacter, Is.EqualTo("DDD"));
			var expectedCounter = verticalRows.Values.ToList();
			Assert.That(expectedCounter.Count, Is.EqualTo(4));
			Assert.That(expectedCounter[0].Values, Is.EqualTo("BB"));
			Assert.That(expectedCounter[1].Values, Is.EqualTo("DD"));
			Assert.That(expectedCounter[2].Values, Is.EqualTo("DDD"));
			Assert.That(expectedCounter[3].Values, Is.EqualTo("DD"));

		}

		[Test]
		public void Must_Correctly_Evaluate_Diagonal_Row()
		{
			var pivotX = 3;
			var pivotY = 0;
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var diagonalRow = expectedMatrix.InternalEvaluateDiagonalRow(pivotX, pivotY);
			Assert.That(diagonalRow, Is.EqualTo("RYCA"));
		}

		[Test]
		public void Must_Correctly_Evaluate_Diagonal_Rows()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			var diagonalRows = expectedMatrix.EvaluateDiagonalRows();
			Assert.That(diagonalRows.MaxCharacter, Is.EqualTo("DDDDD"));
		}

		[Test]
		public void Must_Correctly_Evaluate_Matrix()
		{
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			PrintMatrix(expectedMatrix);
			Console.WriteLine("**************************************************");
			var expectedCounterValues = expectedMatrix.EvaluateMatrix();
			Console.WriteLine($"Resultado: {expectedCounterValues.MaxCharacter}");
			Assert.That(expectedCounterValues.MaxCharacter, Is.EqualTo("DDDDD"));
		}

		[Test]
		public void Must_Correctly_Evaluate_Matrix_Vertical_Character()
		{
			_data = new[] { "HYI3DD3", "HYI3DD3", "HYI3DD3", "H7OÑGD2", "HNSPE0D", "H9CDDEF", "HXDDDJK" };
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			PrintMatrix(expectedMatrix);
			Console.WriteLine("**************************************************");
			var expectedCounterValues = expectedMatrix.EvaluateMatrix();
			Console.WriteLine($"Resultado: {expectedCounterValues.MaxCharacter}");
			Assert.That(expectedCounterValues.MaxCharacter, Is.EqualTo("HHHHHHH"));
		}

		[Test]
		public void Must_Correctly_Evaluate_Matrix_Horizontal_Character()
		{
			_data = new[] { "1YI3DD3", "2YI3DD3", "3YI3DD3", "9999999", "HYSPE0D", "HYCDDEF", "HXDDDJK" };
			var expectedMatrix = MatrixExtensions.CreateMatrix(7, 7, _data);
			PrintMatrix(expectedMatrix);
			Console.WriteLine("**************************************************");
			var expectedCounterValues = expectedMatrix.EvaluateMatrix();
			Console.WriteLine($"Resultado: {expectedCounterValues.MaxCharacter}");
			Assert.That(expectedCounterValues.MaxCharacter, Is.EqualTo("9999999"));
		}


		public static void PrintMatrix(string[,] arr)
		{
			var rowLength = arr.GetLength(0);
			var colLength = arr.GetLength(1);

			for (var x = 0; x < rowLength; x++)
			{
				for (var y = 0; y < colLength; y++)
				{
					var value = arr[x, y];
					Console.Write($"{value}   ");
				}
				Console.Write(Environment.NewLine + Environment.NewLine);
			}
		}
	}
}