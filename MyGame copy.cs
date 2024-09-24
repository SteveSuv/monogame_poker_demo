using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MyDemo;

public class MyGame2 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private FontSystem _fontSystem;

    Texture2D targetSprite;
    Texture2D skySprite;
    Texture2D crosshairsSprite;
    SpriteFont gameFont;
    Vector2 targetPos = new Vector2(300, 300);
    const int rad = 45;

    int score = 0;
    bool isPress = false;

    public MyGame2()
    {
        _graphics = new GraphicsDeviceManager(this);
        // Content.RootDirectory = "Content";
        IsMouseVisible = true;
        // _graphics.IsFullScreen=true;
        // Window.AllowUserResizing = true;
        // Window.ClientSizeChanged += new EventHandler<EventArgs>(OnClientSizeChanged);
        Window.Title = "MyDemo001";

        // Window.FileDrop += new EventHandler<FileDropEventArgs>(OnFile);
        // Console.WriteLine(GraphicsDevice.Adapter.Description);

    }


    // private void OnClientSizeChanged(object sender, EventArgs e)
    // {
    //     Console.WriteLine(sender);
    //     Console.WriteLine(e);
    // }


    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _fontSystem = new FontSystem();
        _fontSystem.AddFont(File.ReadAllBytes(@"Assets/fonts/puhuiti.ttf"));
        FontSystemDefaults.FontResolutionFactor = 2;
        FontSystemDefaults.KernelWidth = 2;
        FontSystemDefaults.KernelHeight = 2;

        // targetSprite = Content.Load<Texture2D>("target");
        // skySprite = Content.Load<Texture2D>("sky");
        // crosshairsSprite = Content.Load<Texture2D>("crosshairs");
        // gameFont = Content.Load<SpriteFont>("galleryFont");

        targetSprite = Texture2D.FromStream(_graphics.GraphicsDevice, new FileStream("Assets/sprites/arrow_basic_e.png", FileMode.Open));

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {

        var updatedBallSpeed = gameTime.TotalGameTime.TotalSeconds;
        // Console.WriteLine(updatedBallSpeed);

        var ms = Mouse.GetState();
        var ks = Keyboard.GetState();

        if (ks.IsKeyDown(Keys.Escape))
        {
            Exit();
        }

        if (ms.LeftButton == ButtonState.Pressed && !isPress)
        {
            var mouseTargetDist = Vector2.Distance(targetPos, ms.Position.ToVector2());
            if (mouseTargetDist < rad)
            {
                score++;

                // var rand = new Random();
                // var newPos = new Vector2(rand.Next(_graphics.PreferredBackBufferWidth), rand.Next(_graphics.PreferredBackBufferHeight));
                targetPos = targetPos + new Vector2(10, 10);

            }
            isPress = true;

        }

        if (ms.LeftButton == ButtonState.Released)
        {
            isPress = false;
        }

        base.Update(gameTime);
    }



    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();
        // _spriteBatch.Draw(skySprite, new Vector2(0, 0), Color.White);

        // _spriteBatch.Draw(targetSprite, targetPos - new Vector2(rad, rad), Color.White);

        // _spriteBatch.DrawString(gameFont, $"Score: {score}", new Vector2(20, 20), Color.Blue);

        // _spriteBatch.DrawString(null,"hello",new Vector2(100,100),new Color(100,100,100));



        // _spriteBatch.Draw(Texture2D.FromFile(_graphics.GraphicsDevice, @"Assets/sprites/star.png", (arr) => { }), new Vector2(100, 100), Color.White);
        _spriteBatch.Draw(targetSprite, new Vector2(100, 100), Color.White);

        _spriteBatch.DrawString(_fontSystem.GetFont(30), "The quick いろは brown\nfox にほへ jumps over\nt🙌h📦e l👏a👏zy dog", new Vector2(0, 0), Color.Yellow, effect: FontSystemEffect.Stroked, effectAmount: 1);


        _spriteBatch.End();

        // TODO: Add your drawing code here

        base.Draw(gameTime);
    }
}
