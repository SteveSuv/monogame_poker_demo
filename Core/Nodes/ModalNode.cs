using Microsoft.Xna.Framework;
using MonoGame.Extended;

class ModalNode : Node
{
    public new Vector2 Size = new(MyGame.ScreenWidth / 2, MyGame.ScreenHeight / 2);

    public ModalNode()
    {
        color = Color.White * 0.8f;
        layerDepth = 1;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.FillRectangle(rectangle: Rectangle, color: color, layerDepth: layerDepth);
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: Color.Green, thickness: 2, layerDepth: layerDepth);
        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}