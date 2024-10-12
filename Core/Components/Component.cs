using Microsoft.Xna.Framework;

abstract class Component
{
    public Node belong;
    public string tag;

    public abstract void Initialize();

    public abstract void Update(GameTime gameTime);
}