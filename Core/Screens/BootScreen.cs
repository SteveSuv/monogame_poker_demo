using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

class BootScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
    };

    public override void Initialize()
    {

        world.ComponentManager.AddComponent(new CountdownComponent()
        {
            intervalSeconds = 2,
            Completed = (_, _) =>
            {
                MyGame.LoadScreen(new StartScreen(game));
            }
        });



        world.NodeManager.AddChild(new LabelNode() { text = "Tommy Games Production", fontSize = 60, color = Color.White, effect = FontSystemEffect.Stroked, localPosition = new(0, 10) }).ComponentManager.AddComponent(new TweenerComponent()
        {
            tweenerAction = (t, node) => t.TweenTo(target: node, expression: e => e.localPosition, toValue: new(0, 0), duration: 1)
        });

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);
        MyGame.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}