using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

class SpriteRegion(Texture2DRegion textureRegion) : Actor
{
    public Texture2DRegion textureRegion = textureRegion;
    public SpriteEffects effects = SpriteEffects.None;

    public override void Draw(GameTime gameTime)
    {

        SpriteBatch.Draw(textureRegion: textureRegion, position: position, color: color, rotation: rotation, origin: origin * Size, scale: scale, effects: effects, layerDepth: layerDepth);

        DrawDebug();

    }

    public override Vector2 GetSize()
    {
        return new(textureRegion.Width, textureRegion.Height);
    }
}