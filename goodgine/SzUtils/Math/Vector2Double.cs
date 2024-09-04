namespace GoodGine.Math;

[Serializable]
public struct Vector2Double
{
    public double x, y;

    public Vector2Double(double ax, double ay)
    {
        x = ax;
        y = ay;
    }

    public static Vector2Double Zero => new Vector2Double(0, 0);

    public double Magnitude
    {
        get
        {
            return Math.Sqrt(SqrMagnitude);
        }
    }

    public double SqrMagnitude => Math.Sqr(x) + Math.Sqr(y);

    public static Vector2Double operator -(Vector2Double a, Vector2Double b)
    {
        return new Vector2Double(a.x - b.x, a.y - b.y);
    }

    public static bool operator !=(Vector2Double a, Vector2Double b)
    {
        return !(a.x == b.x && a.y == b.y);
    }

    public static Vector2Double operator *(Vector2Double a, double x)
    {
        return new Vector2Double(a.x * x, a.y * x);
    }

    public static Vector2Double operator /(Vector2Double a, double x)
    {
        return new Vector2Double(a.x / x, a.y / x);
    }

    public static double Distance(Vector2Double a, Vector2Double b)
    {
        return (a - b).Magnitude;
    }

    public static Vector2Double operator +(Vector2Double a, Vector2Double b)
    {
        return new Vector2Double(a.x + b.x, a.y + b.y);
    }

    public static bool operator ==(Vector2Double a, Vector2Double b)
    {
        return a.x == b.x && a.y == b.y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }
}