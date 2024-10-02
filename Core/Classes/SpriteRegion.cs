using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

class SpriteRegion(Texture2DRegion textureRegion) : Actor
{
    public Texture2DRegion textureRegion = textureRegion;
    public SpriteEffects effects = SpriteEffects.None;

    public override void Draw(GameTime gameTime)
    {

        MyGame.SpriteBatch.Draw(textureRegion: textureRegion, position: position, color: color, rotation: rotation, origin: OriginOffset, scale: scale, effects: effects, layerDepth: layerDepth);
        base.Draw(gameTime);
    }

    public override Vector2 GetSize()
    {
        return new(textureRegion.Width, textureRegion.Height);
    }
}