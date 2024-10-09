using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class ModalNode : Node
{
    public new Vector2 Size = new(MyGame.ScreenWidth / 2, MyGame.ScreenHeight / 2);

    public ModalNode()
    {
        Transform.color = Color.White * 0.9f;
        Transform.layerDepth = 1;

        OnOutSideClick += (object sender, Vector2 mousePos) =>
        {
            parent.RemoveChild(this);
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.FillRectangle(rectangle: Rectangle, color: Transform.color, layerDepth: Transform.layerDepth);
        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}