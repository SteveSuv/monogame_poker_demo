using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Sprite(Texture2D texture) : Actor
{
    public Texture2D texture = texture;
    public SpriteEffects effects = SpriteEffects.None;

    public override void Draw(GameTime gameTime)
    {
        SpriteBatch.Draw(texture: texture, position: position, sourceRectangle: null, color: color, rotation: rotation, origin: origin * Size, scale: scale, effects: effects, layerDepth: layerDepth);
        DrawDebug();
    }

    public override Vector2 GetSize()
    {
        return new(texture.Width, texture.Height);
    }
}