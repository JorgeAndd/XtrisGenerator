using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XtrisGenerator;

namespace XtrisTester
{
	[TestClass]
	public class XTrisChecker
	{
		[TestMethod]
		public void TestRotation1()
		{
			Piece piece = new Piece(new char[,]{ { ' ', 'x', 'x', ' ' },
												 { ' ', ' ', 'x', ' ' },
												 { ' ', ' ', 'x', ' ' },
												 { ' ', ' ', ' ', ' ' }});

			char[,] rotated = null;

			piece.Rotate(ref rotated);

			var expected = new char[,] { { ' ', ' ', ' ', ' ' },
										 { 'x', 'x', 'x', ' ' },
										 { 'x', ' ', ' ', ' ' },
										 { ' ', ' ', ' ', ' ' }};

			CollectionAssert.AreEqual(expected, rotated);
		}

		[TestMethod]
		public void Test360Rotation()
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

			CollectionAssert.AreEqual(expected, piece.board);
		}
	}
}
