using Microsoft.Xna.Framework;
using MonoGame.Extended;

class ButtonNode : Node
{
    public new Vector2 Size = new(100, 50);

    public override void Initialize()
    {
        var SoundButtOnClickComponent = new SoundComponent() { soundEffect = Assets.SoundButtOnClick };
        var SoundButtOnHoverComponent = new SoundComponent() { soundEffect = Assets.SoundButtOnHover };

        ComponentManager.AddComponent(SoundButtOnClickComponent);

        ComponentManager.AddComponent(SoundButtOnHoverComponent);

        ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnMouseEnter = (object sender, Vector2 mousePos) =>
        {
            SoundButtOnHoverComponent.Play();
            color *= 0.8f;
        },

            OnClick = (object sender, Vector2 mousePos) =>
                   {
                       SoundButtOnClickComponent.Play();
                   },


            OnMouseLeave = (object sender, Vector2 mousePos) =>
            {
                color = Color.White;
            }
        });

        base.Initialize();
    }


    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw()
    {
        MyGame.SpriteBatch.FillRectangle(rectangle: Rectangle, color: color, layerDepth: LayerDepth);
        MyGame.SpriteBatch.DrawRectangle(rectangle: Rectangle, color: Color.Black, thickness: 2, layerDepth: LayerDepth);

        base.Draw();
    }

    public override Vector2 GetSize()
    {
        return Size;
    }
}