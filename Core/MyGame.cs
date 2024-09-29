using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Collections;
using MonoGame.Extended;
using MonoGame.Extended.Timers;
using MonoGame.Extended.Input;

class MyGame : Game
{
    public static GraphicsDeviceManager graphics;
    public static SpriteBatch spriteBatch;
    public static GameWindow window;
    public static bool isDebug = false;
    public static int screenWidth => 1920 / 2;
    public static int screenHeight => 1080 / 2;
    public static Color debugColor => Color.Red;

    public static Vector2 screenCenter => new Vector2(screenWidth / 2, screenHeight / 2) - _cameraController.camera.Center;

    private Texture2DAtlas _textureCardsBlackClubs;
    private Texture2DAtlas _textureCardsBlackSpades;
    private Texture2DAtlas _textureCardsRedDiamonds;
    private Texture2DAtlas _textureCardsRedHearts;
    private static CameraController _cameraController;
    private IList<Texture2DRegion> _cardList;
    private ContinuousClock _clock;

    private Sound _sound;

    private PeerServer _peerServer;
    private PeerCient _peerClient;


    public MyGame()
    {
        Window.Title = $"PokerGame1 {screenWidth}x{screenHeight}";
        Window.AllowAltF4 = false;
        Window.AllowUserResizing = false;

        window = Window;

        IsMouseVisible = true;

        graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = screenWidth,
            PreferredBackBufferHeight = screenHeight,
            IsFullScreen = false
        };
        graphics.ApplyChanges();
    }


    protected override void LoadContent()
    {


        _peerServer = new PeerServer();
        _peerClient = new PeerCient();

        _peerServer.Start(9000);
        _peerClient.Connect(port:9000);


        spriteBatch = new SpriteBatch(GraphicsDevice);

        _cameraController = new CameraController();

        _clock = new ContinuousClock(0.1);
        _clock.Pause();

        _sound = new Sound(Assets.SoundMouseClick) { volume = 0.5f };

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

        _clock.Tick += (object sender, EventArgs e) =>
        {
            _cardList = list.Shuffle(Random.Shared);
        };
    }

    protected override void Update(GameTime gameTime)
    {
        var mouse = MouseExtended.GetState();
        var keyboard = KeyboardExtended.GetState();

        if (mouse.WasButtonPressed(MouseButton.Left) && IsActive)
        {
            _sound.Play();
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
                isDebug = !isDebug;
            }

            if (keyboard.WasKeyPressed(Keys.S))
            {
                _cameraController.isShaking = !_cameraController.isShaking;
            }

            if (keyboard.WasKeyPressed(Keys.R))
            {
                _cameraController.resetCamera();
            }

            if (keyboard.WasKeyPressed(Keys.C))
            {
                if (_clock.State == TimerState.Paused)
                {
                    _clock.Start();
                }
                else
                {
                    _clock.Pause();
                }
            }
        }



        // updates
        _cameraController.Update(gameTime);
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

        spriteBatch.Begin(transformMatrix: _cameraController.camera.GetViewMatrix(), blendState: BlendState.AlphaBlend, samplerState: SamplerState.PointClamp);


        var card = new Sprite(_cardList[0]) { position = screenCenter, origin = Origin.center };
        // card.Draw(gameTime);
        if (isDebug)
        {
            spriteBatch.DrawPoint(screenCenter, debugColor, 4);
        }
        _cameraController.Draw(gameTime);

        spriteBatch.End();

        base.Draw(gameTime);
    }
}
