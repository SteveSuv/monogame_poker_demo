using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

class SpriteRegionNode : Node
{
    public Texture2DRegion texture2DRegion = null;
    public SpriteEffects effects = SpriteEffects.None;
    public new Vector2 Size => new(texture2DRegion.Width, texture2DRegion.Height);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.Draw(textureRegion: texture2DRegion, position: WorldPosition, color: color, rotation: rotation, origin: OriginOffset, scale: scale, effects: effects, layerDepth: LayerDepth);
        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}