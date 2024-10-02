using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using FontStashSharp;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

class MyGame : Game
{
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;
    public static GameWindow window;
    public static bool isDebug = false;
    public static int ScreenWidth => 1920 / 2;
    public static int ScreenHeight => 1080 / 2;
    public static Color DebugColor => Color.Red;
    public static Vector2 ScreenCenter => new(ScreenWidth / 2, ScreenHeight / 2);
    public static ScreenManager screenManager;
    // private Texture2DAtlas _textureCardsBlackClubs;
    // private ContinuousClock _clock;
    private Sound _soundMouseClick;
    private Sound _soundBGM;
    private PeerServer _peerServer;
    private PeerCient _peerClient;
    // private int _regionIndex = 0;

    // private Label _title;

    // private SpriteRegion _card;

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

        screenManager = new ScreenManager();
        Components.Add(screenManager);
    }

    protected override void Initialize()
    {

        _peerServer = new PeerServer();
        _peerClient = new PeerCient();

        _peerServer.Start(9000);
        _peerClient.Connect(port: 9000);

        // _cameraController = new CameraController();

        // _clock = new ContinuousClock(0.1);
        // _clock.Pause();

        FontSystemDefaults.FontResolutionFactor = 2;
        FontSystemDefaults.KernelWidth = 2;
        FontSystemDefaults.KernelHeight = 2;


        base.Initialize();
        LoadBootScreen();
    }

    protected override void LoadContent()
    {

        spriteBatch = new SpriteBatch(GraphicsDevice);
        _soundMouseClick = new Sound(Assets.SoundButtonClick) { volume = 0.8f };
        _soundBGM = new Sound(Assets.SoundBGM) { volume = 0.8f };
        _soundBGM.Play();

        // var spriteAtlas = new SpriteAtlas(Assets.TextureCardsBlackClubs) { regionWidth = 88, regionHeight = 124, maxRegionCount = 13 };
        // _textureCardsBlackClubs = spriteAtlas.GetAtlas();

        // _card = new(_textureCardsBlackClubs[_regionIndex]) { position = ScreenCenter, origin = Origin.Center };

        // _title = new(DateTime.Now.ToLongTimeString()) { color = Color.Yellow, effect = FontSystemEffect.Stroked, fontSize = 50, position = ScreenCenter, origin = Origin.BottomRight };

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

        if (keyboard.WasKeyPressed(Keys.C))
        {
            LoadStartScreen();
        }
        else if (keyboard.WasKeyPressed(Keys.V))
        {
            LoadBootScreen();
        }

        // _title.rotation += gameTime.GetElapsedSeconds();
        // _card.rotation += gameTime.GetElapsedSeconds();


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
            // _regionIndex = Random.Shared.Next(0, 13);
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
        // _clock.Update(gameTime);

        _peerServer.Update();
        _peerClient.Update();

        MouseExtended.Update();
        KeyboardExtended.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        // GraphicsDevice.Clear(new Color(0, 128, 128, 100));

        // spriteBatch.Begin(samplerState: SamplerState.PointClamp);

        // _card.Draw(gameTime);
        // _title.Draw(gameTime);

        // spriteBatch.End();

        base.Draw(gameTime);
    }

    private void LoadBootScreen()
    {
        screenManager.LoadScreen(new BootScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
    }

    private void LoadStartScreen()
    {
        screenManager.LoadScreen(new StartScreen(this), new FadeTransition(GraphicsDevice, Color.Black));
    }
}
