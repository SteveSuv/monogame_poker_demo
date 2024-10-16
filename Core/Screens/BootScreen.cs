using FontStashSharp;
using Microsoft.Xna.Framework;
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
            Completed = (object sender, EventArgs e) =>
            {
                MyGame.LoadScreen(new StartScreen(game));
            }
        });

        world.NodeManager.AddChild(new LabelNode() { text = "Tommy Games Production", fontSize = 60, color = Color.White, effect = FontSystemEffect.Stroked });

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);
        MyGame.SpriteBatch.Begin();
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}