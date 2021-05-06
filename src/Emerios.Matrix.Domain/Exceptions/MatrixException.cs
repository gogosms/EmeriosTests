using System;

namespace Emerios.Matrix.Domain.Exceptions
{
	public class MatrixException : Exception
	{
		public MatrixException(int width, int height, string message) : base(message)
		{
			Width = width;
			Height = height;
		}

		public int Width { get; }

		public int Height { get; }

		
	}
}