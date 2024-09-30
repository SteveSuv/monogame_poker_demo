using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using MonoGame.Extended.Input;
using FontStashSharp;


class MyGame : Game
{
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;
    public static GameWindow window;
    public static bool isDebug = true;
    public static int ScreenWidth => 1920 / 2;
    public static int ScreenHeight => 1080 / 2;
    public static Color DebugColor => Color.Red;
    public static Vector2 ScreenCenter => new(ScreenWidth / 2, ScreenHeight / 2);
    private Texture2DAtlas _textureCardsBlackClubs;
    private ContinuousClock _clock;
    private Sound _soundMouseClick;
    private PeerServer _peerServer;
    private PeerCient _peerClient;
    private int _regionIndex = 0;

    private static Label Title => new(DateTime.Now.ToLongTimeString()) { color = Color.Yellow, effect = FontSystemEffect.Stroked, fontSize = 50, position = ScreenCenter, origin = Origin.Center };

    private SpriteRegion Card => new(_textureCardsBlackClubs[_regionIndex]) { position = ScreenCenter, origin = Origin.Center };


    public static void Print(params object[] values)
    {
        var result = string.Join(" ", values);
        Console.WriteLine(result);
    }


    public MyGame()
    {
        Window.Title = $"PokerGame1 {ScreenWidth}x{ScreenHeight}";
        Window.AllowAltF4 = false;
        Window.AllowUserResizing = false;

        window = Window;

        IsMouseVisible = true;

        graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = ScreenWidth,
            PreferredBackBufferHeight = ScreenHeight,
            IsFullScreen = false
        };
        graphics.ApplyChanges();
    }

    protected override void Initialize()
    {

        _peerServer = new PeerServer();
        _peerClient = new PeerCient();

        _peerServer.Start(9000);
        _peerClient.Connect(port: 9000);



        // _cameraController = new CameraController();

        _clock = new ContinuousClock(0.1);
        _clock.Pause();


        base.Initialize();
    }

    protected override void LoadContent()
    {

        spriteBatch = new SpriteBatch(GraphicsDevice);

        _soundMouseClick = new Sound(Assets.SoundMouseClick) { volume = 0.5f };
        _textureCardsBlackClubs = Texture2DAtlas.Create($"Atlas/{Assets.TextureCardsBlackClubs.Name}", Assets.TextureCardsBlackClubs, 88, 124, 13);
        // _textureCardsBlackSpades = Texture2DAtlas.Create("Atlas/TextureCardsBlackSpades", Assets.TextureCardsBlackSpades, 88, 124, 13);
        // _textureCardsRedDiamonds = Texture2DAtlas.Create("Atlas/TextureCardsRedDiamonds", Assets.TextureCardsRedDiamonds, 88, 124, 13);
        // _textureCardsRedHearts = Texture2DAtlas.Create("Atlas/TextureCardsRedHearts", Assets.TextureCardsRedHearts, 88, 124, 13);

        // var list = new List<Texture2DRegion>();
        // list.AddRange([.. _textureCardsBlackClubs]);
        // list.AddRange([.. _textureCardsBlackSpades]);
        // list.AddRange([.. _textureCardsRedDiamonds]);
        // list.AddRange([.. _textureCardsRedHearts]);

        // _cardList = list.Shuffle(Random.Shared);

        // _clock.Tick += (object sender, EventArgs e) =>
        // {
        //     // _cardList = list.Shuffle(Random.Shared);
        //     _regionIndex = Random.Shared.Next(0, 13);
        // };
    }

    protected override void Update(GameTime gameTime)
    {
        var mouse = MouseExtended.GetState();
        var keyboard = KeyboardExtended.GetState();

        if (mouse.WasButtonPressed(MouseButton.Left) && IsActive)
        {
            _soundMouseClick.Play();
        }

        if (keyboard.WasKeyPressed(Keys.Escape))
        {
            Exit();
        }


        if (keyboard.WasKeyPressed(Keys.L))
        {
            _peerClient.SendMessageToAll("hello1");
        }

        if (keyboard.WasKeyPressed(Keys.M))
        {
            // if (_clock.State == TimerState.Paused)
            // {
            //     _clock.Start();
            // }
            // else
            // {
            //     _clock.Pause();
            // }
            _regionIndex = Random.Shared.Next(0, 13);
        }


        if (keyboard.IsControlDown())
        {
            if (keyboard.WasKeyPressed(Keys.D))
            {
                isDebug = !isDebug;
            }

            // if (keyboard.WasKeyPressed(Keys.S))
            // {
            //     _cameraController.isShaking = !_cameraController.isShaking;
            // }

            // if (keyboard.WasKeyPressed(Keys.R))
            // {
            //     _cameraController.resetCamera();
            // }

            if (keyboard.WasKeyPressed(Keys.F))
            {
                graphics.ToggleFullScreen();
            }

        }



        // updates
        // _cameraController.Update(gameTime);
        _clock.Update(gameTime);

        _peerServer.Update();
        _peerClient.Update();

        MouseExtended.Update();
        KeyboardExtended.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(0, 128, 128, 100));

        spriteBatch.Begin(samplerState: SamplerState.PointClamp);




        Card.Draw(gameTime);


        Title.Draw(gameTime);


        // if (isDebug)
        // {
        //     spriteBatch.DrawPoint(screenCenter, debugColor, 4);
        // }

        // _cameraController.Draw(gameTime);

        // Print("123", "哈哈哈", 456, false);

        // spriteBatch.DrawString(Assets.FontPuhuiti.GetFont(20))

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
