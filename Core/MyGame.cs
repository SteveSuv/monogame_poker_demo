using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using FontStashSharp;


class MyGame : Game
{
    private static GraphicsDeviceManager graphics;
    private SpriteBatch _spriteBatch;

    static public MyGame instance;



    private Texture2DAtlas _cards;
    private Camera _camera;

    private KeyboardListener _keyboardListener;


    public MyGame()
    {
        Window.Title = "PokerGame";
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }


    protected override void LoadContent()
    {

        instance = this;

        _camera = new Camera();

        _keyboardListener = new KeyboardListener();
        _keyboardListener.KeyPressed += (sender, eventArgs) =>
        {
            // if (eventArgs.Key == Keys.Enter && _adventurer.CurrentAnimation == "idle")
            // {
            //     _adventurer.SetAnimation("attack");
            // }
            if (eventArgs.Key == Keys.Enter)
            {
                Window.Title = DateTime.Now.ToLongTimeString();
            }
        };


        _spriteBatch = new SpriteBatch(GraphicsDevice);


        _cards = Texture2DAtlas.Create("Atlas/Cards", Assets.TextureCardsBlackClubs, 88, 124);

        // texture2D = _assetsLoader.loadTexture2D("Assets/sprites/arrow_basic_e.png");
        // fontSystem = _assetsLoader.loadFont("Assets/fonts/youquti.ttf");
    }

    protected override void Update(GameTime gameTime)
    {

        Window.Title = DateTime.Now.ToLongTimeString();

        _keyboardListener.Update(gameTime);

        MouseExtended.Update();
        KeyboardExtended.Update();

        var mouse = MouseExtended.GetState();
        var keyboard = KeyboardExtended.GetState();

        if (keyboard.WasKeyPressed(Keys.Escape))
        {
            Console.WriteLine("ESC");
            Exit();
        }


        if (mouse.WasButtonPressed(MouseButton.Right))
        {
            // graphics.ToggleFullScreen();
            // _assetsLoader.playSound(path: "Assets/sounds/btn_click.wav");
        }


        _camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(0, 128, 128, 100));


        _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

        for (int i = 0; i < 13; i++)
        {
            var card = _cards[i];
            _spriteBatch.Draw(card, new Vector2(card.Width * i, 0), Color.White);
        }

        _spriteBatch.DrawCircle(new CircleF(new Vector2(150, 150), 100), 100, Color.Red, 10);
        _spriteBatch.DrawRectangle(new RectangleF(new Vector2(250, 250), new SizeF(50, 50)), Color.Black, 10);
        _spriteBatch.DrawString(Assets.FontYouquti.GetFont(60), "你好呀", new Vector2(200, 200), Color.Purple, 0, characterSpacing: 2, effect: FontSystemEffect.Stroked, effectAmount: 1);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
