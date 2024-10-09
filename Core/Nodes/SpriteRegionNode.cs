using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Graphics;

class SpriteRegionNode : Node
{
    public Texture2DRegion texture2DRegion;
    public SpriteEffects effects = SpriteEffects.None;
    public Vector2 Size => new(texture2DRegion.Width, texture2DRegion.Height);
    public Vector2 OriginOffset => Transform.origin * Size;

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.Draw(textureRegion: texture2DRegion, position: Transform.WorldPosition, color: Transform.color, rotation: Transform.rotation, origin: OriginOffset, scale: Transform.scale, effects: effects, layerDepth: Transform.layerDepth);
        base.Draw();
    }
}