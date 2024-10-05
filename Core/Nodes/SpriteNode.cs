using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteNode : Node
{
    public Texture2D texture;
    public SpriteEffects effects = SpriteEffects.None;
    public Vector2 Size => new(texture.Width, texture.Height);
    public Vector2 OriginOffset => transform.origin * Size;
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.Draw(texture: texture, position: transform.worldPosition, sourceRectangle: null, color: transform.color, rotation: transform.rotation, origin: OriginOffset, scale: transform.scale, effects: effects, layerDepth: transform.layerDepth);
        base.Draw();
    }
}