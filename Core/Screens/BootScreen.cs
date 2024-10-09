using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Timers;

class BootScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new()
    {
        Transform = { localPosition = MyGame.ScreenCenter },
        children = [
            new LabelNode() { text = "Tommy Games Production", fontSize = 60 }
        ]
    };
    private readonly CountdownTimer _countdownTimer = new(2);

    public override void LoadContent()
    {
        base.LoadContent();

        _countdownTimer.Completed += (object sender, EventArgs e) =>
        {
            MyGame.LoadScreen(new StartScreen(game));
        };
    }

    public override void Update(GameTime gameTime)
    {
        _countdownTimer.Update(gameTime);
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