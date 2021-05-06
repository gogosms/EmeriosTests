using System;

namespace Emerios.Matrix.Domain.Exceptions
{
	public class MatrixInvalidDataException : Exception
	{
		public MatrixInvalidDataException() : base("The matrix must have data")
		{
		}
	}
}