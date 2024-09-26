using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using MonoGame.Extended.Input.InputListeners;
using MonoGame.Extended.ViewportAdapters;


public class MyGame : Game
{
    // private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private AssetsLoader _assetsLoader;

    // Texture2D texture2D;

    // FontSystem fontSystem;


    private Texture2DAtlas _cards;
    private OrthographicCamera _camera;

    private KeyboardListener _keyboardListener;


    public MyGame()
    {
        Window.Title = "德州扑克";
        new GraphicsDeviceManager(this);
        IsMouseVisible = true;

    }

    // protected override void Initialize()
    // {

    // }

    protected override void LoadContent()
    {

        var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
        _camera = new OrthographicCamera(viewportAdapter);

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
        _assetsLoader = new AssetsLoader(GraphicsDevice);

        _cards = Texture2DAtlas.Create("Atlas/Cards", _assetsLoader.loadTexture2D("Assets/textures/cards/Clubs-88x124.png"), 88, 124);

        // texture2D = _assetsLoader.loadTexture2D("Assets/sprites/arrow_basic_e.png");
        // fontSystem = _assetsLoader.loadFont("Assets/fonts/youquti.ttf");
    }

    protected override void Update(GameTime gameTime)
    {

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


        if (mouse.WasButtonPressed(MouseButton.Left))
        {
            // _assetsLoader.playSound(path: "Assets/sounds/btn_click.wav");
        }

        MoveCamera(gameTime);
        ZoomCamera(gameTime);
        RotateCamera(gameTime);
        PitchCamera(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SlateGray);

        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix);


        // _spriteBatch.Draw(texture: texture2D, position: new Vector2(100, 100), color: Color.White);
        // _spriteBatch.DrawString(font: fontSystem.GetFont(30), text: "你是谁", position: new Vector2(0, 0), color: Color.Yellow, effect: FontSystemEffect.Stroked, effectAmount: 1);
        for (int i = 0; i < 13; i++)
        {
            var card = _cards[i];
            _spriteBatch.Draw(card, new Vector2(card.Width * i, 0), Color.White);
        }

        _spriteBatch.DrawCircle(new CircleF(new Vector2(150, 150), 100), 100, Color.Red, 10);
        _spriteBatch.DrawRectangle(new RectangleF(new Vector2(250, 250), new SizeF(50, 50)), Color.Black, 10);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void MoveCamera(GameTime gameTime)
    {
        var dir = Vector2.Zero;
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 500 : 200;


        if (keyboard.IsKeyDown(Keys.W))
        {
            dir -= Vector2.UnitY;
        }
        if (keyboard.IsKeyDown(Keys.A))
        {
            dir -= Vector2.UnitX;
        }
        if (keyboard.IsKeyDown(Keys.S))
        {
            dir += Vector2.UnitY;
        }
        if (keyboard.IsKeyDown(Keys.D))
        {
            dir += Vector2.UnitX;
        }


        _camera.Move(dir * speed * gameTime.GetElapsedSeconds());

    }

    private void ZoomCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.Z))
        {
            _camera.ZoomIn(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.X))
        {
            _camera.ZoomOut(speed * gameTime.GetElapsedSeconds());
        }
    }


    private void RotateCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var speed = keyboard.IsShiftDown() ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.Q))
        {
            _camera.Rotate(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.E))
        {
            _camera.Rotate(-speed * gameTime.GetElapsedSeconds());
        }
    }


    private void PitchCamera(GameTime gameTime)
    {
        var keyboard = KeyboardExtended.GetState();
        var isShiftDown = keyboard.IsShiftDown();
        var speed = isShiftDown ? 5 : 1;

        if (keyboard.IsKeyDown(Keys.R))
        {
            _camera.PitchUp(speed * gameTime.GetElapsedSeconds());
        }
        if (keyboard.IsKeyDown(Keys.T))
        {
            _camera.PitchDown(speed * gameTime.GetElapsedSeconds());
        }
    }


}
