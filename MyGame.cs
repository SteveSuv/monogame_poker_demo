using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Graphics;
using MonoGame.Extended.Input;
using MonoGame.Extended.ViewportAdapters;


public class MyGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private AssetsLoader _assetsLoader;

    // Texture2D texture2D;

    // FontSystem fontSystem;


    private Texture2DAtlas _cards;
    private OrthographicCamera _camera;


    public MyGame()
    {
        Window.Title = "德州扑克";
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;

    }
    
    protected override void LoadContent()
    {

        var viewportAdapter = new BoxingViewportAdapter(Window, GraphicsDevice, 800, 480);
        _camera = new OrthographicCamera(viewportAdapter);

        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _assetsLoader = new AssetsLoader(GraphicsDevice);

        _cards = Texture2DAtlas.Create("Atlas/Cards", _assetsLoader.loadTexture2D("Assets/textures/cards/Clubs-88x124.png"), 88, 124);

        // texture2D = _assetsLoader.loadTexture2D("Assets/sprites/arrow_basic_e.png");
        // fontSystem = _assetsLoader.loadFont("Assets/fonts/youquti.ttf");
    }

    protected override void Update(GameTime gameTime)
    {

        MouseExtended.Update();
        KeyboardExtended.Update();
        var mouseState = MouseExtended.GetState();
        var keyboardState = KeyboardExtended.GetState();

        if (keyboardState.WasKeyPressed(Keys.Escape))
        {
            Console.WriteLine("ESC");
            Exit();
        }

        if (mouseState.WasButtonPressed(MouseButton.Left))
        {
            // _assetsLoader.playSound(path: "Assets/sounds/btn_click.wav");
        }
        var movementSpeed = keyboardState.IsKeyDown(Keys.LeftShift) ? 500 : 200;
        _camera.Move(GetMovementDirection() * movementSpeed * gameTime.GetElapsedSeconds());

        // Add this to the Update() method
        AdjustZoom();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SlateGray);

        var transformMatrix = _camera.GetViewMatrix();
        _spriteBatch.Begin(transformMatrix: transformMatrix);


        // _spriteBatch.Draw(texture: texture2D, position: new Vector2(100, 100), color: Color.White);
        // _spriteBatch.DrawString(font: fontSystem.GetFont(30), text: "你是谁", position: new Vector2(0, 0), color: Color.Yellow, effect: FontSystemEffect.Stroked, effectAmount: 1);
        for (int i = 0; i < 3 * 5; i++)
        {
            var card = _cards[i];
            _spriteBatch.Draw(card, new Vector2(card.Width * i, 0), Color.White);
        }

        // _spriteBatch.DrawCircle(new Vector2(0, 500), 150, 4, Color.Red);
        _spriteBatch.DrawRectangle(new RectangleF(250, 250, 50, 50), Color.Black, 1f);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private Vector2 GetMovementDirection()
    {
        var movementDirection = Vector2.Zero;
        var state = Keyboard.GetState();
        if (state.IsKeyDown(Keys.Down))
        {
            movementDirection += Vector2.UnitY;
        }
        if (state.IsKeyDown(Keys.Up))
        {
            movementDirection -= Vector2.UnitY;
        }
        if (state.IsKeyDown(Keys.Left))
        {
            movementDirection -= Vector2.UnitX;
        }
        if (state.IsKeyDown(Keys.Right))
        {
            movementDirection += Vector2.UnitX;
        }
        return movementDirection;
    }

    // Add this to the Game1.cs file
    private void AdjustZoom()
    {
        var state = Keyboard.GetState();
        float zoomPerTick = state.IsKeyDown(Keys.LeftShift) ? 0.05f : 0.01f;
        if (state.IsKeyDown(Keys.Z))
        {
            _camera.ZoomIn(zoomPerTick);
        }
        if (state.IsKeyDown(Keys.X))
        {
            _camera.ZoomOut(zoomPerTick);
        }
    }



}
