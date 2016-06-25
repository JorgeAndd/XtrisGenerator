using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XtrisGenerator
{
	public struct Point
	{
		public int x;
		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public override string ToString()
		{
			return '[' + x.ToString() + ", " + y.ToString() + ']';
		}
	}

	public class Piece
	{
		public char[,] board;
		private int dimension;

		public Piece(char[,] board)
		{
			if (board.GetLength(0) == board.GetLength(1))
			{
				dimension = board.GetLength(0);
				
				this.board = board;
			}
			else
			{
				throw new ArgumentException("board must be a square matrix");
			}
		}

		// Copy constructor
		public Piece(Piece original)
		{
			this.board = original.board;
			this.dimension = original.dimension;
		}

		public static bool operator ==(Piece p1, Piece p2)
		{
			if (p1.dimension != p2.dimension)
				return false;

			Point? firstBlock1;
			Point? firstBlock2;

			List<Point> blocks1 = new List<Point>(p1.dimension);
			List<Point> blocks2 = new List<Point>(p2.dimension);

			int rotations = 0;

			Piece p2_copy = new Piece(p2);

			while(rotations < 4)
			{
				firstBlock1 = null;
				firstBlock2 = null;

				// Find first position with block on p1 and p2
				for (int i = 0; i < p1.dimension; i++)
				{
					for (int j = 0; j < p1.dimension; j++)
					{
						if (p1.board[i, j] != ' ')
						{
							blocks1.Add(new Point(i, j));

							if (firstBlock1 == null)
							{
								firstBlock1 = new Point(i, j);
							}
						}

						if (p2_copy.board[i, j] != ' ')
						{
							blocks2.Add(new Point(i, j));

							if (firstBlock2 == null)
							{
								firstBlock2 = new Point(i, j);
							}
						}
					}
				}

				// Calculate the offset from the top rightmost block
				int offset_x = firstBlock1.Value.x - firstBlock2.Value.x;
				int offset_y = firstBlock1.Value.y - firstBlock2.Value.y;

				bool different = false;

				for (int i = 0; i < p1.dimension; i++)
				{
					if (blocks1[i].x - blocks2[i].x != offset_x
						|| blocks1[i].y - blocks2[i].y != offset_y)
					{
						p2_copy.Rotate();
						rotations++;
						different = true;

						blocks1.Clear();
						blocks2.Clear();

						break;
					}
				}

				if(!different)
					return true;
			}

			return false;
		}

		public static bool operator !=(Piece p1, Piece p2)
		{
			return !(p1 == p2);
		}


		// Rotate the piece and updated it's object
		public void Rotate()
		{
			char[,] rotated = new char[dimension, dimension];

			for(int i = 0; i < this.board.GetLength(0); i++)
			{
				for(int j = 0; j < this.board.GetLength(1); j++)
				{
					rotated[dimension - j - 1, i] = this.board[i, j];
				}
			}

			this.board = rotated;
		}

		// Rotate a piece and return it as parameter
		public void Rotate(ref char[,] rotated)
		{
			rotated = new char[dimension, dimension];

			for (int i = 0; i < this.board.GetLength(0); i++)
			{
				for (int j = 0; j < this.board.GetLength(1); j++)
				{
					rotated[dimension - j - 1, i] = this.board[i, j];
				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Piece p1 = new Piece(new char[,]{ { 'x', 'x', 'x', 'x' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p2 = new Piece(new char[,]{ { 'x', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' }});

			var eq = p1 == p2;

			return;

		}
	}
}
