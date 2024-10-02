using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class Button : Actor
{
    public float thickness => Size.Y / 2;
    public EventHandler Hover;
    private bool isHover = false;
    public EventHandler Click;
    public Sound hoverSound = new(Assets.SoundButtonHover) { volume = 0.8f };
    public Sound clickSound = new(Assets.SoundButtonClick) { volume = 0.8f };

    public override void Update(GameTime gameTime)
    {

        if (Rectangle.Intersects(MyGame.MouseRectangle))
        {
            if (!isHover)
            {
                hoverSound.Play();
                Hover?.Invoke(this, null);
                isHover = true;
            }

            if (MyGame.MouseState.WasButtonPressed(MouseButton.Left) && MyGame.IsActive)
            {
                clickSound.Play();
                Click?.Invoke(this, null);
            }

        }
        else
        {
            isHover = false;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: color, thickness: thickness, layerDepth: layerDepth);
        base.Draw(gameTime);
    }

    public override Vector2 GetSize()
    {
        return new(100, 50);
    }
}