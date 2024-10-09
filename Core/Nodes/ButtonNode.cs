using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class ButtonNode : Node
{
    public new Vector2 Size = new(100, 50);
    private readonly Sound hoverSound = new(Assets.SoundButtonHover) { volume = 0.8f };
    private readonly Sound clickSound = new(Assets.SoundButtonClick) { volume = 0.8f };

    public ButtonNode()
    {
        OnHover += (object sender, Vector2 mousePos) =>
        {
            hoverSound.Play();
            Transform.color *= 0.8f;
        };

        OnClick += (object sender, Vector2 mousePos) =>
        {
            clickSound.Play();
        };

        OnLeave += (object sender, Vector2 mousePos) =>
        {
            Transform.color = Color.White;
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