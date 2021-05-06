namespace Emerios.Matrix.Domain.Exceptions
{
	public class MatrixInvalidSizeException: MatrixException
	{
		public MatrixInvalidSizeException(int width, int height) 
			: base(width, height, $"The Height:{height} and Width: {width} must be greater than 1.")
		{
		}
	}
}