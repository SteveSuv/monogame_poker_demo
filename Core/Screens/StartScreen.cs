using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{
    private new MyGame Game => (MyGame)base.Game;

    private Sprite _logo;
    private Label _title;

    public override void LoadContent()
    {
        base.LoadContent();
        _logo = new Sprite(Assets.TextureLogo) { position = MyGame.ScreenCenter, origin = Origin.Center, scale = new Vector2(0.3f) };
        _title = new Label("二十一点") { position = MyGame.ScreenCenter + new Vector2(0, 120), origin = Origin.Center, fontSize = 40 };
    }

    public override void Update(GameTime gameTime)
    {
    }

    public override void Draw(GameTime gameTime)
    {
        Game.GraphicsDevice.Clear(Color.Black);
        MyGame.spriteBatch.Begin();
        _logo.Draw(gameTime);
        _title.Draw(gameTime);
        MyGame.spriteBatch.End();
    }
}