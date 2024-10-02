using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Tweening;

class StartScreen(MyGame game) : GameScreen(game)
{

    private Sprite _logo;
    private Label _title;
    private readonly Tweener _logoTweener = new();
    private readonly Tweener _titleTweener = new();
    public override void LoadContent()
    {
        base.LoadContent();

        _logo = new Sprite(Assets.TextureLogo) { position = MyGame.ScreenCenter + new Vector2(-100, -120), origin = Origin.Center, scale = new Vector2(0.3f), color = Color.Black };

        _title = new Label("二十一点") { position = MyGame.ScreenCenter + new Vector2(0, 200), origin = Origin.Center, fontSize = 40, color = Color.Black };

        _logoTweener.TweenTo(target: _logo, expression: e => e.position, toValue: _logo.position + new Vector2(200, 0), duration: 1f).Easing(EasingFunctions.SineInOut).AutoReverse().RepeatForever();

        _logoTweener.TweenTo(target: _logo, expression: e => e.color, toValue: Color.White, duration: 1f).Easing(EasingFunctions.Linear).AutoReverse().RepeatForever();


        _titleTweener.TweenTo(target: _title, expression: e => e.position, toValue: MyGame.ScreenCenter, duration: 2f).Easing(EasingFunctions.SineInOut);

        _titleTweener.TweenTo(target: _title, expression: e => e.color, toValue: Color.White, duration: 2f).Easing(EasingFunctions.Linear);

    }

    public override void Update(GameTime gameTime)
    {
        _logoTweener.Update(gameTime.GetElapsedSeconds());
        _titleTweener.Update(gameTime.GetElapsedSeconds());
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);

        MyGame.SpriteBatch.Begin();

        _logo.Draw(gameTime);
        _title.Draw(gameTime);

        MyGame.SpriteBatch.End();
    }
}