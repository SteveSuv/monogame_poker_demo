using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

abstract class Actor
{
    // game
    protected static SpriteBatch SpriteBatch => MyGame.spriteBatch;
    protected static bool IsDebug => MyGame.isDebug;
    protected static Color DebugColor => MyGame.DebugColor;

    protected static GameWindow Window => MyGame.window;
    protected static GraphicsDeviceManager Graphics => MyGame.graphics;

    //common
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
        if (IsDebug)
        {
            SpriteBatch.DrawRectangle(Rectangle, DebugColor);
            SpriteBatch.DrawPoint(position: Rectangle.Center, color: DebugColor, size: 4);
        }
    }
}
