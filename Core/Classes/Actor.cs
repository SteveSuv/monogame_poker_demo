using Microsoft.Xna.Framework;
using MonoGame.Extended;

abstract class Actor
{
    public Vector2 position = Vector2.Zero;
    public Color color = Color.White;
    public float rotation = 0;
    public Vector2 origin = Origin.TopLeft;
    public Vector2 scale = Vector2.One;
    public float layerDepth = 0;
    public Vector2 Size => GetSize();
    public Vector2 OriginOffset => origin * Size;
    public RectangleF Rectangle => new(position - OriginOffset, Size);

    public virtual void Update(GameTime gameTime) { }

    public virtual void Draw(GameTime gameTime)
    {
        DrawDebug();
    }

    public abstract Vector2 GetSize();

    private void DrawDebug()
    {
        if (MyGame.IsDebug)
        {
            MyGame.SpriteBatch.DrawRectangle(Rectangle, MyGame.DebugColor);
            MyGame.SpriteBatch.DrawPoint(position: Rectangle.Center, color: MyGame.DebugColor, size: 4);
        }
    }
}
