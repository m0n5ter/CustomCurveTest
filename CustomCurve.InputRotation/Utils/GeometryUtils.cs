using System;
using Avalonia;

namespace CustomCurve.InputRotation.Utils;

public static class GeometryUtils
{
    public static Point? GetIntersectionSegmentLine(Point p1, Point p2, Point p3, Point p4)
    {
        var x1 = p1.X;
        var y1 = p1.Y;
        var x2 = p2.X;
        var y2 = p2.Y;

        var x3 = p3.X;
        var y3 = p3.Y;
        var x4 = p4.X;
        var y4 = p4.Y;

        var d = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

        if (d == 0)
            return null;

        var intersectX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / d;
        var intersectY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / d;

        var minX = Math.Min(x1, x2);
        var maxX = Math.Max(x1, x2);
        var minY = Math.Min(y1, y2);
        var maxY = Math.Max(y1, y2);

        return intersectX >= minX && intersectX <= maxX && intersectY >= minY && intersectY <= maxY
            ? new Point(intersectX, intersectY)
            : null;
    }

    public static void ClipOrExtendToRectangle(Rect rect, ref Point? p0, ref Point? p1)
    {
        if (p0 == null) throw new ArgumentNullException(nameof(p0));
        if (p1 == null) throw new ArgumentNullException(nameof(p1));

        var leftPoint = (p0.Value.X < p1.Value.X ? p0 : p1).Value;
        var rightPoint = (p0.Value.X > p1.Value.X ? p0 : p1).Value;

        var leftIntersection = GetIntersectionSegmentLine(rect.BottomLeft, rect.TopLeft, leftPoint, rightPoint);
        var rightIntersection = GetIntersectionSegmentLine(rect.BottomRight, rect.TopRight, leftPoint, rightPoint);

        if (leftIntersection == null || rightIntersection == null)
        {
            p0 = p1 = null;
        }
        else if (p0.Value.X < p1.Value.X)
        {
            p0 = leftIntersection;
            p1 = rightIntersection;
        }
        else
        {
            p1 = leftIntersection;
            p0 = rightIntersection;

        }
    }
}