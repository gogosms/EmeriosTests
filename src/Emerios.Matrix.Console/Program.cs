using System;
using System.Diagnostics;
using Emerios.Matrix.Domain;

namespace Emerios.Matrix.Console
{
	public class Program
	{
		private const int WidthAndHeightMatrix = 7;

		static void Main(string[] args)
		{
			System.Console.WriteLine("Reto: Encuentra la subcadena más larga presente en una matriz");
			var data = new[] { "BBDADEF", "BXCDDJK", "HYI3DD3", "R7OÑGD2", "WNSPE0D", "A9CDDEF", "BXDDDJK" };
			var expectedMatrix = MatrixExtensions.CreateMatrix(WidthAndHeightMatrix, WidthAndHeightMatrix, data);
			System.Console.WriteLine("---------------------------------------");
			PrintMatrix(expectedMatrix);
			System.Console.WriteLine("---------------------------------------");
			System.Console.WriteLine("Debería devolver la cadena D, D, D, D, D, porque hay una diagonal de D de longitud 5 que es la más larga");
			System.Console.WriteLine("Presione una tecla para evaluar la matrix.");
			System.Console.ReadLine();
			System.Console.WriteLine("Evaluando Matrix  ..........................");

			var stopwatch = new Stopwatch();
			stopwatch.Start();
			var counterValues = expectedMatrix.EvaluateMatrix(WidthAndHeightMatrix);
			stopwatch.Stop();
			System.Console.WriteLine($"Resultado: {counterValues.MaxCharacter}, tiempo: {stopwatch.ElapsedMilliseconds} ms. ");
			System.Console.ReadLine();
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
					if (x == 0 && y == 2 && value.Equals("D")
						|| x == 1 && y == 3 && value.Equals("D")
						|| x == 2 && y == 4 && value.Equals("D")
						|| x == 3 && y == 5 && value.Equals("D")
						|| x == 4 && y == 6 && value.Equals("D"))
					{
						System.Console.ForegroundColor = ConsoleColor.DarkGreen;
					}
					else
					{
						System.Console.ForegroundColor = ConsoleColor.White;
					}
					System.Console.Write($"{value}   ");
				}
				System.Console.Write(Environment.NewLine + Environment.NewLine);
			}
		}
	}
}
