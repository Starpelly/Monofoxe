﻿using System;
using Microsoft.Xna.Framework;

namespace Monofoxe.Engine.Utils
{
	/// <summary>
	/// Contains useful math stuff. 
	/// </summary>
	public static class GameMath
	{
		#region Math.

		public static double Lerp(double a, double b, double value) =>
			a + (b - a) * value;

		public static float Round2Nearest(float a, float interval)
		{
			return a - (a % interval);
		}


		#endregion Math.


		#region Distance.

		/// <summary>
		/// Calculates distance between two points.
		/// </summary>
		public static float Distance(Vector2 p1, Vector2 p2) =>
			(p2 - p1).Length();
		
		#endregion Distance.



		#region Intersestions.

		/// <summary>
		/// Checks if a point lies within a rectangle.
		/// </summary>
		public static bool PointInRectangle(Vector2 point, Vector2 rectPoint1, Vector2 rectPoint2) =>	
			point.X >= rectPoint1.X && point.X <= rectPoint2.X && point.Y >= rectPoint1.Y && point.Y <= rectPoint2.Y;
		
		/// <summary>
		/// Checks if a point lies within a rectangle.
		/// </summary>
		public static bool PointInRectangleBySize(Vector2 point, Vector2 rectCenter, Vector2 rectSize)
		{
			var rectHalfSize = rectSize / 2f;
			var pt1 = rectCenter - rectHalfSize;
			var pt2 = rectCenter + rectHalfSize;
			return point.X >= pt1.X && point.X <= pt2.X && point.Y >= pt1.Y && point.Y <= pt2.Y;
		}
		

		/// <summary>
		/// Checks if a point lies within a triangle.
		/// </summary>
		public static bool PointInTriangle(Vector2 point, Vector2 triPoint1, Vector2 triPoint2, Vector2 triPoint3)
		{
			Vector2 p = point - triPoint1;
			Vector2 v2 = triPoint2 - triPoint1;
			Vector2 v3 = triPoint3 - triPoint1;
			
			float w1 = (triPoint1.X * v3.Y - point.X * v3.Y + p.Y * v3.X) / (v2.Y * v3.X - v2.X * v3.Y); 
			float w2 = (point.Y - triPoint1.Y - w1 * v2.Y) / v3.Y;
			
			return (w1 >= 0 && w2 >= 0 && ((w1 + w2) <= 1));
		}

		/// <summary>
		/// Checks if two rectangles intersect.
		/// </summary>
		public static bool RectangleInRectangle(Vector2 rect1Pt1, Vector2 rect1Pt2, Vector2 rect2Pt1, Vector2 rect2Pt2) =>
			rect1Pt1.X <= rect2Pt2.X && rect1Pt2.X >= rect2Pt1.X && rect1Pt1.Y <= rect2Pt2.Y && rect1Pt2.Y >= rect2Pt1.Y;
		
		/// <summary>
		/// Checks if two rectangles intersect.
		/// </summary>
		public static bool RectangleInRectangleBySize(Vector2 rect1Center, Vector2 rect1Size, Vector2 rect2Center, Vector2 rect2Size)
		{
			var delta = rect2Center - rect1Center;
			var size = (rect2Size + rect1Size) / 2f; 

			return Math.Abs(delta.X) <= size.X && Math.Abs(delta.Y) <= size.Y; 
		}

		/// <summary>
		/// Calculates on which side point is from a line. 
		/// 1 - left
		/// -1 - right
		/// 0 - on the line
		/// </summary>
		public static int PointSide(Vector2 point, Vector2 linePt1, Vector2 linePt2)
		{
			var v = new Vector2(linePt2.Y - linePt1.Y, linePt1.X - linePt2.X);

			return Math.Sign(Vector2.Dot(point - linePt1, v));
		}
		
		/// <summary>
		/// Checks if two lines cross. Returns 1 if lines cross, 0 if not and 2 if lines overlap.
		/// </summary>
		public static int LinesCross(Vector2 line1Pt1, Vector2 line1Pt2, Vector2 line2Pt1, Vector2 line2Pt2)
		{
			var line1 = new Vector2(line1Pt2.Y - line1Pt1.Y, line1Pt1.X - line1Pt2.X);
			var line2 = new Vector2(line2Pt2.Y - line2Pt1.Y, line2Pt1.X - line2Pt2.X);
			
			var side1 = Math.Sign(Vector2.Dot(line2Pt1 - line1Pt1, line1));
			var side2 = Math.Sign(Vector2.Dot(line2Pt2 - line1Pt1, line1));
			var side3 = Math.Sign(Vector2.Dot(line1Pt1 - line2Pt1, line2));
			var side4 = Math.Sign(Vector2.Dot(line1Pt2 - line2Pt1, line2));

			if (side1 != side2 && side3 != side4)
			{
				return 1;
			}

			if (side1 == 0 && side2 == 0)
			{
				return 2;
			}
						
			return 0;
		}

		
		/// <summary>
		/// Checks if two linew cross. Returns 1 if lines cross, 0 if not and 2 if lines overlap.
		/// Also calculates intersection point.
		/// </summary>
		public static int LinesCross(Vector2 line1Pt1, Vector2 line1Pt2, Vector2 line2Pt1, Vector2 line2Pt2, ref Vector2 collisionPt)
		{
			int result = LinesCross(line1Pt1, line1Pt2, line2Pt1, line2Pt2);
			if (result == 1)
			{
				Vector2 e1 = line1Pt2 - line1Pt1;
				e1.Normalize();
				Vector2 e2 = line2Pt2 - line2Pt1;
				e2.Normalize();

				// A bit of unpleasant math.
				// The base idea is:
				// e1 * l1 + v1 = e2 * l2 + v2
				// So we need to calculate l1 or l2 and plop it into vector function.

				// Formula will probably break with parallel lines.
				// (result == 1) prevents it, but just keep this in mind.
				float l = (e1.X * (line1Pt1.Y - line2Pt1.Y) + e1.Y * (line2Pt1.X - line1Pt1.X)) / (e1.X * e2.Y - e2.X * e1.Y);

				collisionPt = line2Pt1 + e2 * l;
			}
			return result;
		}

		#endregion Intersections.
	}
}
