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

		[TestMethod]
		public void ValidRotation()
		{
			Piece p1 = new Piece(new char[,]{ { 'x', 'x', ' ', ' ' },
											  { 'x', 'x', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			p1.Rotate();

			Piece p2 = new Piece(new char[,]{ { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { 'x', 'x', ' ', ' ' },
											  { 'x', 'x', ' ', ' ' }});

			CollectionAssert.AreEqual(p1.board, p2.board);
		}

		[TestMethod]
		public void TestEqual()
		{
			Piece p1 = new Piece(new char[,]{ { ' ', 'x', 'x', 'x' },
											  { ' ', ' ', 'x', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p2 = new Piece(new char[,]{ { ' ', ' ', ' ', ' ' },
											  { 'x', 'x', 'x', 'x' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p3 = new Piece(new char[,]{ { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { 'x', 'x', 'x', 'x' }});

			Piece p4 = new Piece(new char[,]{ { ' ', ' ', ' ', ' ' },
											  { ' ', 'x', 'x', ' ' },
											  { ' ', 'x', 'x', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p5 = new Piece(new char[,]{ { 'x', 'x', ' ', ' ' },
											  { 'x', 'x', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Assert.AreEqual(false, p1 == p2);
			Assert.AreEqual(true, p2 == p3);
			Assert.AreEqual(true, p4 == p5);
			Assert.AreEqual(false, p1 == p5);
		}

		[TestMethod]
		public void TestEqualWithRotation()
		{
			Piece p1 = new Piece(new char[,]{ { 'x', 'x', 'x', 'x' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p2 = new Piece(new char[,]{ { 'x', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' }});

			Assert.AreEqual(true, p1 == p2);

			Piece p3 = new Piece(new char[,]{ { ' ', 'x', 'x', 'x' },
											  { ' ', ' ', 'x', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p4 = new Piece(new char[,]{ { ' ', ' ', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' },
											  { 'x', 'x', ' ', ' ' },
											  { 'x', ' ', ' ', ' ' }});

			Assert.AreEqual(true, p3 == p4);

			Piece p5 = new Piece(new char[,]{ { ' ', ' ', 'x', 'x' },
											  { ' ', 'x', 'x', ' ' },
											  { ' ', ' ', ' ', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Piece p6 = new Piece(new char[,]{ { ' ', 'x', ' ', ' ' },
											  { ' ', 'x', 'x', ' ' },
											  { ' ', ' ', 'x', ' ' },
											  { ' ', ' ', ' ', ' ' }});

			Assert.AreEqual(true, p5 == p6);

		}
	}
}
