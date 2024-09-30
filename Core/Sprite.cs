using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

class Sprite : Actor
{

    public Texture2D texture;
    public Vector2 position = Vector2.Zero;
    public Color color = Color.White;
    public float rotation = 0;
    public Vector2 origin = Origin.leftTop;
    public Vector2 scale = Vector2.One;
    public SpriteEffects effects = SpriteEffects.None;
    public float layerDepth = 0;
    public Vector2 size => new(texture.Width, texture.Height);
    public RectangleF rectangle => new RectangleF(position - origin * size, size);


    public Sprite(Texture2D texture)
    {
        this.texture = texture;
    }

    public override void Update(GameTime gameTime)
    {

    }
    public override void Draw(GameTime gameTime)
    {

        spriteBatch.Draw(texture: texture, position: position, sourceRectangle: null, color: color, rotation: rotation, origin: origin * size, scale: scale, effects: effects, layerDepth: layerDepth);

        if (isDebug)
        {
            spriteBatch.DrawRectangle(rectangle, debugColor);
            spriteBatch.DrawPoint(position: rectangle.Center, color: debugColor, size: 4);
        }
    }
}