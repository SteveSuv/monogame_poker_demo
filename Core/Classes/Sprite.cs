using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class Sprite(Texture2D texture) : Actor
{
    public Texture2D texture = texture;
    public SpriteEffects effects = SpriteEffects.None;

    public override void Draw(GameTime gameTime)
    {
        MyGame.SpriteBatch.Draw(texture: texture, position: position, sourceRectangle: null, color: color, rotation: rotation, origin: OriginOffset, scale: scale, effects: effects, layerDepth: layerDepth);
        base.Draw(gameTime);
    }

    public override Vector2 GetSize()
    {
        return new(texture.Width, texture.Height);
    }
}