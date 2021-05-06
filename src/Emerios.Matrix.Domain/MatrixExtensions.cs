using System;
using System.Collections.Generic;
using System.Linq;
using Emerios.Matrix.Domain.Exceptions;
using Emerios.Matrix.Domain.Models;

namespace Emerios.Matrix.Domain
{
	public static class MatrixExtensions
	{
		public static string[,] CreateMatrix(int width, int height, string[] values = null)
		{

			if (width <= 1 && height <= 1)
			{
				throw new MatrixInvalidSizeException(width, height);
			}

			if (!width.Equals(height))
			{
				throw new MatrixSquareException(width, height);
			}

			if (values is null)
			{
				throw new MatrixInvalidDataException();
			}

			var matrix = new string[width, height];

			for (var i = 0; i < values.Length; i++)
			{
				var value = values[i];
				for (int j = 0; j < value.Length; j++)
				{
					var pivot = value[j];
					matrix[i, j] = pivot.ToString();
				}
			}

			return matrix;
		}

		public static CounterValues EvaluateMatrix(this string[,] matrix, int length = 7)
		{
			var horizontalRows = EvaluateRows(i => matrix.GetHorizontalOrVertical(i));
			var verticalRows = EvaluateRows(i => matrix.GetHorizontalOrVertical(i, false));
			var diagonalRows = matrix.EvaluateDiagonalRows(length);
			var counterFinal = CreateReport(horizontalRows, verticalRows, diagonalRows);
			return counterFinal;
		}

		public static string GetHorizontalOrVertical(this string[,] matrix, int pivot, bool usePivotX = true,
			int length = 7)
		{
			var rows = string.Empty;
			for (var i = 0; i < length; i++)
			{
				var row = usePivotX ? matrix[pivot, i] : matrix[i, pivot];
				rows += row;
			}
			return rows;
		}

		public static CounterValues EvaluateRows(Func<int, string> evaluateDirection,
			int length = 7)
		{
			var counters = new List<CounterValue>();
			for (var i = 0; i < length; i++)
			{
				var rowAsString = evaluateDirection(i);
				var counter = EvaluateString(rowAsString);
				if (counter != null)
				{
					counters.Add(counter);
				}
			}
			return new CounterValues(counters);
		}

		public static CounterValues EvaluateDiagonalRows(this string[,] matrix, int length = 7)
		{
			var listValues = new List<string>();

			for (int i = 0; i < length; i++)
			{
				listValues.Add(matrix.InternalEvaluateDiagonalRow(i, 0));
			}

			for (var i = 0; i < length; i++)
			{
				var item = matrix.InternalEvaluateDiagonalRow(length, i);
				listValues.Add(item);
			}

			for (var i = length - 1; i >= 0; i--)
			{
				listValues.Add(matrix.InternalEvaluateDiagonalReverseRow(0, i));
			}

			for (var i = length - 1; i >= 0; i--)
			{
				listValues.Add(matrix.InternalEvaluateDiagonalReverseRow(i, 0));
			}

			var finalDiagonals = listValues.Where(l => l.Length > 0).Distinct();

			var counters = new List<CounterValue>();
			foreach (var diagonal in finalDiagonals)
			{
				var longestRun = diagonal.EvaluateLongestRun();
				if (longestRun.Length == 1)
				{
					continue; 
				}
				counters.Add(new CounterValue { Values = longestRun });
			}


			return new CounterValues(counters);

		}

		internal static string InternalEvaluateDiagonalReverseRow(this string[,] matrix, int pivotX, int pivotY)
		{
			var listValues = new List<string>();
			var pivot = matrix.GetCharacter(pivotX, pivotY);
			listValues.Add(pivot);

			string nextCharacter;
			do
			{
				pivotX += 1;
				pivotY += 1;
				nextCharacter = matrix.GetCharacter(pivotX, pivotY);
				if (!string.IsNullOrEmpty(nextCharacter))
				{
					listValues.Add(nextCharacter);
				}

			} while (nextCharacter != null);

			return string.Join(string.Empty, listValues);
		}

		internal static string InternalEvaluateDiagonalRow(this string[,] matrix, int pivotX, int pivotY)
		{
			var listValues = new List<string>();
			var pivot = matrix.GetCharacter(pivotX, pivotY);
			listValues.Add(pivot);

			string nextCharacter;
			do
			{
				pivotX -= 1;
				pivotY += 1;
				nextCharacter = matrix.GetCharacter(pivotX, pivotY);
				if (!string.IsNullOrEmpty(nextCharacter))
				{
					listValues.Add(nextCharacter);
				}

			} while (nextCharacter != null);

			return string.Join(string.Empty, listValues);
		}

		public static string GetCharacter(this string[,] matrix, int pivotX, int pivotY)
		{
			try
			{
				var value = matrix.GetValue(pivotX, pivotY);
				return value?.ToString();
			}
			catch
			{
				return null;
			}
			
		}
		
		public static CounterValue EvaluateString(this string rowAsString)
		{
			var longestRun = rowAsString.EvaluateLongestRun();
			return longestRun.Length == 1 ? null : new CounterValue { Values = longestRun };
		}
		

		public static CounterValues CreateReport(CounterValues horizontalRows, CounterValues verticalRows, CounterValues diagonalRows)
		{
			var values = new List<CounterValue>();
			values.AddRange(horizontalRows.Values);
			values.AddRange(verticalRows.Values);
			values.AddRange(diagonalRows.Values);
			var counterFinal = new CounterValues(values);
			return counterFinal;

		}

	}
}