using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input;
using MonoGame.Extended.Screens;
using MonoGame.Extended.Screens.Transitions;

class MyGame : Game
{
    public static GraphicsDeviceManager Graphics;
    public static new GraphicsDevice GraphicsDevice => Graphics.GraphicsDevice;
    public static SpriteBatch SpriteBatch;
    public static new GameWindow Window;
    public static MouseStateExtended MouseState => MouseExtended.GetState();
    public static KeyboardStateExtended KeyboardState => KeyboardExtended.GetState();
    public static Vector2 MousePos => MouseState.Position.ToVector2();
    public static bool IsDebug = false;
    public static new bool IsActive = false;
    public static readonly int ScreenWidth = 1920 / 2;
    public static readonly int ScreenHeight = 1080 / 2;
    public static readonly Color DebugColor = Color.Red;
    public static readonly Vector2 ScreenCenter = new(ScreenWidth / 2, ScreenHeight / 2);
    public static readonly ScreenManager ScreenManager = new();
    public static readonly Peer Peer = new();

    public Node world = new();

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
            IsFullScreen = false,
        };
        Graphics.ApplyChanges();
        Components.Add(ScreenManager);
    }

    protected override void Initialize()
    {
        base.Initialize();
        LoadScreen(new BootScreen(this));
    }

    protected override void LoadContent()
    {
        SpriteBatch = new(base.GraphicsDevice);
        world.ComponentManager.AddComponent(new SoundComponent() { soundEffect = Assets.SoundBGM, autoPlay = true });
        world.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        world.Update(gameTime);

        Peer.Update(gameTime);
        MouseExtended.Update();
        KeyboardExtended.Update();
        IsActive = base.IsActive;
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        world.Draw();
        base.Draw(gameTime);
    }

    public static void LoadScreen(Screen screen, float duration = 1)
    {
        ScreenManager.LoadScreen(screen, new FadeTransition(GraphicsDevice, Color.Black, duration));
    }
}
