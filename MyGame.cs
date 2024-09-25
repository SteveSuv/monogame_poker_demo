using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace MyDemo;

public class MyGame : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private AssetsLoader _assetsLoader;

    Texture2D texture2D;

    FontSystem fontSystem;

    public MyGame()
    {
        Window.Title = "仲夏夜之梦";
        _graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
    }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _assetsLoader = new AssetsLoader(GraphicsDevice);

        texture2D = _assetsLoader.loadTexture2D("Assets/sprites/arrow_basic_e.png");
        fontSystem = _assetsLoader.loadFont("Assets/fonts/youquti.ttf");
    }

    protected override void BeginRun()
    {
        _assetsLoader.playSound(path: "Assets/sounds/menu.wav", isLooped: true);
        base.BeginRun();
    }

    protected override void Update(GameTime gameTime)
    {

        MouseExtended.Update();
        var mouseState = MouseExtended.GetState();
        var keyboardState = Keyboard.GetState();

        if (keyboardState.IsKeyDown(Keys.Escape))
        {
            Console.WriteLine("ESC");
            Exit();
        }

        if (mouseState.WasButtonPressed(MouseButton.Left))
        {
            _assetsLoader.playSound(path: "Assets/sounds/btn_click.wav");
        }

        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.SlateGray);

        _spriteBatch.Begin();


        _spriteBatch.Draw(texture: texture2D, position: new Vector2(100, 100), color: Color.White);

        _spriteBatch.DrawString(font: fontSystem.GetFont(30), text: "The quick いろは brown\nfox にほへ jumps over\nt🙌h📦e l👏a👏zy dog", position: new Vector2(0, 0), color: Color.Yellow, effect: FontSystemEffect.Stroked, effectAmount: 1);


        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
