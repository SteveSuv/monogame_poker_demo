using Microsoft.Xna.Framework;

static class Origin
{
    public static readonly Vector2 TopLeft = new(0, 0);
    public static readonly Vector2 TopCenter = new(0.5f, 0);
    public static readonly Vector2 TopRight = new(1, 0);

    public static readonly Vector2 CenterLeft = new(0, 0.5f);
    public static readonly Vector2 Center = new(0.5f, 0.5f);
    public static readonly Vector2 CenterRight = new(1, 0.5f);

    public static readonly Vector2 BottomLeft = new(0, 1);
    public static readonly Vector2 BottomCenter = new(0.5f, 1);
    public static readonly Vector2 BottomRight = new(1, 1);
}