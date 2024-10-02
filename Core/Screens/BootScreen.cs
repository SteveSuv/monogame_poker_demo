using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Timers;

class BootScreen(MyGame game) : GameScreen(game)
{
    private readonly Label _title = new("Tommy Games Production") { position = MyGame.ScreenCenter, origin = Origin.Center, fontSize = 60 };
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
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);
        MyGame.SpriteBatch.Begin();
        _title.Draw(gameTime);
        MyGame.SpriteBatch.End();
    }
}