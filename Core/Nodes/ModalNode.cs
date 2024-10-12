using Microsoft.Xna.Framework;
using MonoGame.Extended;

class ModalNode : Node
{
    public new Vector2 Size = new(MyGame.ScreenWidth / 2, MyGame.ScreenHeight / 2);

    public override void Initialize()
    {
        color = Color.White * 0.8f;
        LayerDepth = 1;
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.FillRectangle(rectangle: Rectangle, color: color, layerDepth: LayerDepth);
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: Color.Green, thickness: 2, layerDepth: LayerDepth);
        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}