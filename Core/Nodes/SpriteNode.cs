using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

class SpriteNode : Node
{
    public Texture2D texture;
    public SpriteEffects effects = SpriteEffects.None;
    public Vector2 Size => new(texture.Width, texture.Height);
    public Vector2 OriginOffset => Transform.origin * Size;
    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.Draw(texture: texture, position: Transform.WorldPosition, sourceRectangle: null, color: Transform.color, rotation: Transform.rotation, origin: OriginOffset, scale: Transform.scale, effects: effects, layerDepth: Transform.layerDepth);
        base.Draw();
    }
}