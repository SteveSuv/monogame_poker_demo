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
    public Vector2 origin = Origin.LeftTop;
    public Vector2 scale = Vector2.One;
    public float layerDepth = 0;
    public Vector2 Size => GetSize();
    public RectangleF Rectangle => new(position - origin * Size, Size);

    public virtual void Update(GameTime gameTime) { }

    public abstract void Draw(GameTime gameTime);

    public abstract Vector2 GetSize();

    public void DrawDebug()
    {
        if (IsDebug)
        {
            SpriteBatch.DrawRectangle(Rectangle, DebugColor);
            SpriteBatch.DrawPoint(position: Rectangle.Center, color: DebugColor, size: 4);
        }
    }
}
