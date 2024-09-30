using Microsoft.Xna.Framework;

static class Origin
{
    public static Vector2 LeftTop => new(0, 0);
    public static Vector2 LeftCenter => new(0, 0.5f);
    public static Vector2 LeftBottom => new(0, 1);
    public static Vector2 CenterTop => new(0.5f, 0);
    public static Vector2 Center => new(0.5f, 0.5f);
    public static Vector2 CenterBottom => new(0.5f, 1);
    public static Vector2 RightTop => new(1, 0);
    public static Vector2 RightCenter => new(1, 0.5f);
    public static Vector2 RightBottom => new(1, 1);
}