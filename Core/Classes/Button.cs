using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class Button() : Actor
{
    public required Label label;
    private Label FinalLabel
    {
        get
        {
            label.position = position;
            label.origin = Origin.Center;
            label.color = Color.Black;
            return label;
        }
    }
    public float Thickness => Size.Y / 2;
    private bool isHover = false;
    public EventHandler Hover;
    public EventHandler Click;
    public Sound hoverSound = new(Assets.SoundButtonHover) { volume = 0.8f };
    public Sound clickSound = new(Assets.SoundButtonClick) { volume = 0.8f };
    public Color hoverColor = new(200, 200, 200);
    public Color ClickColor = new(200, 200, 200);

    public override void Update(GameTime gameTime)
    {
        FinalLabel.Update(gameTime);

        if (Rectangle.Intersects(MyGame.MouseRectangle))
        {
            if (!isHover)
            {
                hoverSound.Play();
                color = hoverColor;
                Hover?.Invoke(this, null);
                isHover = true;
            }

            if (MyGame.MouseState.WasButtonPressed(MouseButton.Left) && MyGame.IsActive)
            {
                clickSound.Play();
                color = ClickColor;
                Click?.Invoke(this, null);
            }

        }
        else
        {
            isHover = false;
            color = Color.White;
        }
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: color, thickness: Thickness, layerDepth: layerDepth);
        FinalLabel.Draw(gameTime);
        base.Draw(gameTime);
    }

    public override Vector2 GetSize()
    {
        return new(100, 50);
    }
}