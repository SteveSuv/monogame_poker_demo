using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;




class Sprite : Actor
{

    public Texture2DRegion texture;
    public Vector2 position = Vector2.Zero;
    public Color color = Color.White;
    public float rotation = 0;
    public Vector2 origin = Origin.leftTop;
    public Vector2 scale = Vector2.One;
    public SpriteEffects effects = SpriteEffects.None;
    public float layerDepth = 0;
    public SizeF size => new(texture.Width, texture.Height);
    public RectangleF rectangle => new(position - origin * size, size);

    public Sprite(Texture2DRegion _texture)
    {
        texture = _texture;

    }
    public override void Update(GameTime gameTime)
    {

    }
    public override void Draw(GameTime gameTime)
    {
        spriteBatch.Draw(texture, position, color, rotation, origin * size, scale, effects, layerDepth);

        if (isDebug)
        {
            spriteBatch.DrawRectangle(rectangle, debugColor);
        }
    }
}