namespace Emerios.Matrix.Domain.Exceptions
{

	public class MatrixSquareException : MatrixException
	{
		public MatrixSquareException(int width, int height) 
			: base(width, height, $"It is not a square matrix. The Height: {height} must equal to Width: {width}.")
		{
		}
	}
}