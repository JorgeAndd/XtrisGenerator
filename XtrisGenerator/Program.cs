using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XtrisGenerator
{
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

		public static bool operator ==(Piece p1, Piece p2)
		{
			return false;
		}

		public static bool operator !=(Piece p1, Piece p2)
		{
			return false;
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
			Piece piece = new Piece(new char[,]{ { ' ', 'x', 'x', ' ' },
												 { ' ', ' ', 'x', ' ' },
												 { ' ', ' ', 'x', ' ' },
												 { ' ', ' ', ' ', ' ' }});

			var expected = new char[,]{ { ' ', 'x', 'x', ' ' },
										{ ' ', ' ', 'x', ' ' },
										{ ' ', ' ', 'x', ' ' },
										{ ' ', ' ', ' ', ' ' }};

			// Do 4 rotations, object should stay the same
			piece.Rotate();
			piece.Rotate();
			piece.Rotate();
			piece.Rotate();

		}
	}
}
