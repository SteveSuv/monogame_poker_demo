using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

abstract class Actor
{
    protected static SpriteBatch spriteBatch => MyGame.spriteBatch;
    protected static bool isDebug => MyGame.isDebug;
    protected static Color debugColor => MyGame.debugColor;
    
    protected static GameWindow window => MyGame.window;
    protected static GraphicsDeviceManager graphics => MyGame.graphics;

    public abstract void Update(GameTime gameTime);
    public abstract void Draw(GameTime gameTime);
}
