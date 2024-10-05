using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Input;

class ButtonNode : Node
{
    private bool isHover = false;
    public EventHandler Hover;
    public EventHandler Click;
    private readonly Sound hoverSound = new(Assets.SoundButtonHover) { volume = 0.8f };
    private readonly Sound clickSound = new(Assets.SoundButtonClick) { volume = 0.8f };
    public Color hoverColor = new(200, 200, 200);
    public Color ClickColor = new(200, 200, 200);
    public Vector2 size = new(100, 50);
    public float Thickness => size.Y / 2;
    public Vector2 OriginOffset => transform.origin * size;
    public RectangleF Rectangle => new(transform.worldPosition - OriginOffset, size);

    public override void Update(GameTime gameTime)
    {

        if (Rectangle.Intersects(MyGame.MouseRectangle))
        {
            if (!isHover)
            {
                hoverSound.Play();
                transform.color = hoverColor;
                Hover?.Invoke(this, null);
                isHover = true;
            }

            if (MyGame.MouseState.WasButtonPressed(MouseButton.Left) && MyGame.IsActive)
            {
                clickSound.Play();
                transform.color = ClickColor;
                Click?.Invoke(this, null);
            }

        }
        else
        {
            isHover = false;
            transform.color = Color.White;
        }

        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: transform.color, thickness: Thickness, layerDepth: transform.layerDepth);
        base.Draw();
    }
}