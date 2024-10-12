using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteNode : Node
{
    public Texture2D texture;
    public SpriteEffects effects = SpriteEffects.None;
    public new Vector2 Size => new(texture.Width, texture.Height);

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.Draw(texture: texture, position: WorldPosition, sourceRectangle: null, color: color, rotation: rotation, origin: OriginOffset, scale: scale, effects: effects, layerDepth: LayerDepth);
        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}