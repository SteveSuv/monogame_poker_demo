using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;
using MonoGame.Extended.Timers;
class BootScreen(MyGame game) : GameScreen(game)
{
    private new MyGame Game => (MyGame)base.Game;

    private Label _title;

    private CountdownTimer _countdownTimer;

    public override void LoadContent()
    {
        base.LoadContent();
        _title = new Label("Tommy Games Production") { position = MyGame.ScreenCenter, origin = Origin.Center, fontSize = 70 };
        _countdownTimer = new CountdownTimer(3);
        
        _countdownTimer.Completed += (object sender, EventArgs e) =>
        {
            MyGame.screenManager.LoadScreen(new StartScreen(game), new FadeTransition(GraphicsDevice, Color.Black));
        };
    }

    public override void Update(GameTime gameTime)
    {
        _countdownTimer.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.Black);
        MyGame.spriteBatch.Begin();
        _title.Draw(gameTime);
        MyGame.spriteBatch.End();
    }
}