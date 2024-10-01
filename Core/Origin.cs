using Microsoft.Xna.Framework;

static class Origin
{
    public static Vector2 TopLeft => new(0, 0);
    public static Vector2 TopCenter => new(0.5f, 0);
    public static Vector2 TopRight => new(1, 0);

    public static Vector2 CenterLeft => new(0, 0.5f);
    public static Vector2 Center => new(0.5f, 0.5f);
    public static Vector2 CenterRight => new(1, 0.5f);

    public static Vector2 BottomLeft => new(0, 1);
    public static Vector2 BottomCenter => new(0.5f, 1);
    public static Vector2 BottomRight => new(1, 1);
}