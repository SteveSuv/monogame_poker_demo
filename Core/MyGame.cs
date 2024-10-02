using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using FontStashSharp;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

class MyGame : Game
{
    public static GraphicsDeviceManager Graphics;
    public static new GraphicsDevice GraphicsDevice => Graphics.GraphicsDevice;
    public static SpriteBatch SpriteBatch;
    public static new GameWindow Window;
    public static bool IsDebug = false;
    public static int ScreenWidth => 1920 / 2;
    public static int ScreenHeight => 1080 / 2;
    public static Color DebugColor => Color.Red;
    public static Vector2 ScreenCenter => new(ScreenWidth / 2, ScreenHeight / 2);
    public static ScreenManager ScreenManager;
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
        base.Window.Title = $"PokerGame1 {ScreenWidth}x{ScreenHeight}";
        base.Window.AllowAltF4 = false;
        base.Window.AllowUserResizing = false;

        Window = base.Window;

        IsMouseVisible = true;

        Graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = ScreenWidth,
            PreferredBackBufferHeight = ScreenHeight,
            IsFullScreen = false
        };
        Graphics.ApplyChanges();

        ScreenManager = new ScreenManager();
        Components.Add(ScreenManager);
    }

    protected override void Initialize()
    {



        _peerServer = new PeerServer();
        _peerClient = new PeerCient();

        _peerServer.Start(port: 9000);
        _peerClient.Connect(port: 9000);

        // _cameraController = new CameraController();
        // _clock = new ContinuousClock(0.1);
        // _clock.Pause();

        FontSystemDefaults.FontResolutionFactor = 2;
        FontSystemDefaults.KernelWidth = 2;
        FontSystemDefaults.KernelHeight = 2;


        base.Initialize();

        LoadScreen(new BootScreen(this));
    }

    protected override void LoadContent()
    {

        SpriteBatch = new SpriteBatch(base.GraphicsDevice);

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
            LoadScreen(new StartScreen(this));
        }

        if (keyboard.WasKeyPressed(Keys.V))
        {
            LoadScreen(new BootScreen(this));
        }

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


        if (keyboard.IsControlDown())
        {
            if (keyboard.WasKeyPressed(Keys.D))
            {
                IsDebug = !IsDebug;
            }


            if (keyboard.WasKeyPressed(Keys.F))
            {
                Graphics.ToggleFullScreen();
            }

        }



        _peerServer.Update();
        _peerClient.Update();

        MouseExtended.Update();
        KeyboardExtended.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);
    }

    public static void LoadScreen(Screen screen, float duration = 1)
    {
        ScreenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, duration));
    }
}
