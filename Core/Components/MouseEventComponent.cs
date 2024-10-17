using Microsoft.Xna.Framework;
using MonoGame.Extended.Input;

class MouseEventComponent : Component
{
    public bool isHovering = false;

    public EventHandler<Vector2> OnMouseEnter = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseDown = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseUp = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnClick = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseMove = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnMouseLeave = (object sender, Vector2 mousePos) => { };
    public EventHandler<Vector2> OnOutSideClick = (object sender, Vector2 mousePos) => { };

    public override void Initialize()
    {
    }

    public override void Update(GameTime gameTime)
    {
        CheckHoverState();
    }

    private void CheckHoverState()
    {

        if (MyGame.IsActive)
        {
            if (belong.Rectangle.Contains(MyGame.MousePos))
            {
                if (!MyGame.hoveringNodes.Contains(belong))
                {
                    MyGame.hoveringNodes.Add(belong);
                }
            }
            else
            {
                MyGame.hoveringNodes.Remove(belong);
            }

            if (MyGame.hoveringNodes.Contains(belong))
            {
                var depth = MyGame.hoveringNodes.Max(e => e.LayerDepth);

                if (belong.LayerDepth == depth)
                {
                    if (!isHovering)
                    {
                        isHovering = true;
                        OnMouseEnter.Invoke(this, MyGame.MousePos);
                    }

                    if (MyGame.MouseState.WasButtonPressed(MouseButton.Left))
                    {
                        OnMouseDown.Invoke(this, MyGame.MousePos);
                    }

                    if (MyGame.MouseState.WasButtonReleased(MouseButton.Left))
                    {
                        OnMouseUp.Invoke(this, MyGame.MousePos);
                        OnClick.Invoke(this, MyGame.MousePos);
                    }

                    OnMouseMove.Invoke(this, MyGame.MousePos);
                }
                else
                {
                    if (isHovering)
                    {
                        isHovering = false;
                        OnMouseLeave.Invoke(this, MyGame.MousePos);
                    }

                    if (MyGame.MouseState.WasButtonPressed(MouseButton.Left))
                    {
                        OnOutSideClick.Invoke(this, MyGame.MousePos);
                    }
                }
            }
        }
    }
}

