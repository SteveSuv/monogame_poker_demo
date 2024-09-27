using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using MonoGame.Extended.Collections;


class MyGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    static public MyGame instance;

    private Texture2DAtlas _textureCardsBlackClubs;
    private Texture2DAtlas _textureCardsBlackSpades;
    private Texture2DAtlas _textureCardsRedDiamonds;
    private Texture2DAtlas _textureCardsRedHearts;
    private Camera _camera;

    // private KeyboardListener _keyboardListener;

    private IList<Texture2DRegion> _cardList;


    public MyGame()
    {
        Window.Title = "PokerGame";
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }


    protected override void LoadContent()
    {

        instance = this;

        _camera = new Camera();

        // _keyboardListener = new KeyboardListener();
        // _keyboardListener.KeyPressed += (sender, eventArgs) =>
        // {
        //     if (eventArgs.Key == Keys.Enter && _adventurer.CurrentAnimation == "idle")
        //     {
        //         _adventurer.SetAnimation("attack");
        //     }
        // };


        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _textureCardsBlackClubs = Texture2DAtlas.Create("Atlas/TextureCardsBlackClubs", Assets.TextureCardsBlackClubs, 88, 124, 13);
        _textureCardsBlackSpades = Texture2DAtlas.Create("Atlas/TextureCardsBlackSpades", Assets.TextureCardsBlackSpades, 88, 124, 13);
        _textureCardsRedDiamonds = Texture2DAtlas.Create("Atlas/TextureCardsRedDiamonds", Assets.TextureCardsRedDiamonds, 88, 124, 13);
        _textureCardsRedHearts = Texture2DAtlas.Create("Atlas/TextureCardsRedHearts", Assets.TextureCardsRedHearts, 88, 124, 13);

        var list = new List<Texture2DRegion>();
        list.AddRange([.. _textureCardsBlackClubs]);
        list.AddRange([.. _textureCardsBlackSpades]);
        list.AddRange([.. _textureCardsRedDiamonds]);
        list.AddRange([.. _textureCardsRedHearts]);

        _cardList = list.Shuffle(Random.Shared);
    }

    protected override void Update(GameTime gameTime)
    {

        Window.Title = DateTime.Now.ToLongTimeString();

        // _keyboardListener.Update(gameTime);

        MouseExtended.Update();
        KeyboardExtended.Update();

        var mouse = MouseExtended.GetState();
        var keyboard = KeyboardExtended.GetState();

        if (keyboard.WasKeyPressed(Keys.Escape))
        {
            Console.WriteLine("ESC");
            Exit();
        }


        // if (mouse.WasButtonPressed(MouseButton.Right))
        // {
        //     graphics.ToggleFullScreen();
        //     _assetsLoader.playSound(path: "Assets/sounds/btn_click.wav");
        // }

        if (keyboard.WasKeyPressed(Keys.M))
        {
            _cardList = _cardList.Shuffle(Random.Shared);
        }


        _camera.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(0, 128, 128, 100));


        _spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix(), blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);

        for (int i = 0; i < 5; i++)
        {
            var card = _cardList[i];

            _spriteBatch.Draw(card, new Vector2(_graphics.PreferredBackBufferWidth / 2 + card.Width * i, _graphics.PreferredBackBufferHeight / 2), Color.White);
        }

        // _spriteBatch.DrawCircle(new CircleF(new Vector2(150, 150), 100), 100, Color.Red, 10);
        // _spriteBatch.DrawRectangle(new RectangleF(new Vector2(250, 250), new SizeF(50, 50)), Color.Black, 10);
        // _spriteBatch.DrawString(Assets.FontYouquti.GetFont(60), "你好呀", new Vector2(200, 200), Color.Purple, 0, characterSpacing: 2, effect: FontSystemEffect.Stroked, effectAmount: 1);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
