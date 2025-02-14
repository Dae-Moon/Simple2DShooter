using System.Security.Permissions;

namespace Simple2DShooter;

internal static class MathUtilities
{
    public static Random random = new();

    public static float NextSingle(this Random rnd, float min, float max)
    {
        double range = (double)max - (double)min;
        double sample = rnd.NextDouble();
        double scaled = min + sample * range;
        return (float)scaled;
    }

    public static Vector2 RandomVector(Vector2 min, Vector2 max)
    {
        var x = random.NextSingle(min.X, max.X);
        var y = random.NextSingle(min.Y, max.Y);
        return new(x, y);
    }

    public static float Normalized(this float angle)
    {
        if (angle < 0)
            angle = angle + 360 * ((int)(angle / 360) + 1);
        else if (angle >= 360)
            angle = angle - 360 * ((int)(angle / 360));

        return angle;
    }

    public static float ToAngle(this Vector2 vector)
    {
        var radians = MathF.Atan2(vector.Y, vector.X);

        return radians * (180f / MathF.PI);
    }

    public static Vector2 ToDirection(this float angle)
    {
        angle = angle.Normalized() * (MathF.PI / 180f);
        var cos = MathF.Cos(angle);
        var sin = MathF.Sin(angle);
        return new(cos, sin);
    }

    public static bool Intersects(this Vector2 position, float radius, Vector2 start, Vector2 end)
    {
        // Calculate the closest point on the line segment to the circle's center
        Vector2 lineDir = end - start;
        Vector2 toCircle = position - start;
        float t = Vector2.Dot(toCircle, lineDir) / Vector2.Dot(lineDir, lineDir);
        t = Math.Clamp(t, 0, 1);
        Vector2 closestPoint = start + t * lineDir;

        // Check if the distance from the closest point to the circle's center is less than the radius
        float distanceSquared = Vector2.DistanceSquared(closestPoint, position);
        return distanceSquared <= radius * radius;
    }

    public static Point ToPoint(this Vector2 vector2) => new((int)vector2.X, (int)vector2.Y);
    public static PointF ToPointF(this Vector2 vector2) => new(vector2.X, vector2.Y);

    public static Vector2 ToVector(this Point point) => new(point.X, point.Y);
    public static Vector2 ToVector(this PointF point) => new(point.X, point.Y);
    public static Vector2 ToVector(this Size size) => new(size.Width, size.Height);
    public static Vector2 ToVector(this SizeF size) => new(size.Width, size.Height);
}
