using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XtrisGenerator
{
	public enum Direction { Up, Right, Down, Left};

	public struct Point
	{
		public int x;
		public int y;

		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Point(Point point, Direction dir)
		{
			switch(dir)
			{
				case Direction.Up:
					this.x = point.x;
					this.y = point.y - 1;
					break;
				case Direction.Right:
					this.x = point.x + 1;
					this.y = point.y;
					break;
				case Direction.Down:
					this.x = point.x;
					this.y = point.y + 1;
					break;
				case Direction.Left:
					this.x = point.x - 1;
					this.y = point.y;
					break;
				default:
					this.x = -1;
					this.y = -1;
					break;
			}
		}

		/// <summary>
		/// Checks if a point is valid in a square matrix of a given dimension
		/// </summary>
		/// <param name="dimension">Square matrix dimension</param>
		/// <returns>True if the point is valid</returns>
		public bool IsValid(int dimension)
		{
			if (y >= 0 &&
				x >= 0 &&
				x < dimension &&
				y < dimension)
				return true;

			return false;
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
				this.board = new char[dimension, dimension];

				Array.Copy(board, this.board, board.Length);
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

		public static List<Piece> GeneratePieces(int nblocks)
		{
			var pieces = new List<Piece>();
			var zeroedBoard = new char[nblocks, nblocks];
			Piece.ZeroBoard(ref zeroedBoard);

			RecursiveGeneration(new Point(0, 1), zeroedBoard, nblocks, ref pieces);

			return pieces;
		}

		private static void RecursiveGeneration(Point point, char[,] board, int remaining, ref List<Piece> pieces)
		{
			board[point.x, point.y] = 'x';
			remaining--;

			if (remaining > 0)
			{
				var dimension = board.GetLength(0);

				// Up
				var newPoint = new Point(point, Direction.Up);
				if (newPoint.IsValid(dimension) && board[newPoint.x, newPoint.y] == ' ')
				{
					RecursiveGeneration(newPoint, board, remaining, ref pieces);
					board[newPoint.x, newPoint.y] = ' ';
				}

				// Right
				newPoint = new Point(point, Direction.Right);
				if (newPoint.IsValid(dimension) && board[newPoint.x, newPoint.y] == ' ')
				{
					RecursiveGeneration(newPoint, board, remaining, ref pieces);
					board[newPoint.x, newPoint.y] = ' ';
				}

				// Down
				newPoint = new Point(point, Direction.Down);
				if (newPoint.IsValid(dimension) && board[newPoint.x, newPoint.y] == ' ')
				{
					RecursiveGeneration(newPoint, board, remaining, ref pieces);
					board[newPoint.x, newPoint.y] = ' ';
				}

				// Left
				newPoint = new Point(point, Direction.Left);
				if (newPoint.IsValid(dimension) && board[newPoint.x, newPoint.y] == ' ')
				{
					RecursiveGeneration(newPoint, board, remaining, ref pieces);
					board[newPoint.x, newPoint.y] = ' ';
				}
			}
			else
			{
				// Checks if the new piece is different than already generated ones
				var newPiece = new Piece(board);
				//Piece.ZeroBoard(ref board);

				foreach (var piece in pieces)
				{
					if (piece == newPiece)
						return;
				}

				pieces.Add(newPiece);
			}
		}

		
		public static void ZeroBoard(ref char[,] arr)
		{
			for (int i = 0; i < arr.GetLength(0); i++)
			{
				for (int j = 0; j < arr.GetLength(1); j++)
				{
					arr[i, j] = ' ';
				}
			}
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			var pieces = Piece.GeneratePieces(4);

			return;

		}
	}
}
