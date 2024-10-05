using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{
    private Node world = new()
    {
        transform = { localPosition = MyGame.ScreenCenter },
        children = [
        new SpriteNode() { texture = Assets.TextureLogo, transform = { localPosition = new(0, -100), scale = new(0.2f) } },
            new LabelNode() { text = "二十一点", fontSize = 40 },
            new ButtonNode()
            {
                tag = "CreateRoom",
                transform = { localPosition = new(0, 100) },
                children = [
            new LabelNode() { text = "创建房间", transform = { color = Color.Black } },
                ]
            },
            new ButtonNode()
            {
                tag = "JoinRoom",
                transform = { localPosition = new(0, 160) },
                children = [
            new LabelNode() { text = "加入房间", transform = { color = Color.Black } },
                ]
            },
        ]
    };
    private ButtonNode _createServerButton => world.GetChildByTag<ButtonNode>("CreateRoom");
    private ButtonNode _connectServerButton => world.GetChildByTag<ButtonNode>("JoinRoom");
    public override void LoadContent()
    {
        base.LoadContent();

        _createServerButton.Click += (object sender, EventArgs e) =>
        {
            MyGame.NetworkManager.StartServer();
            MyGame.NetworkManager.ConnectServer();
            MyGame.LoadScreen(new LobbyScreen(game));
        };

        _connectServerButton.Click += (object sender, EventArgs e) =>
        {
            // MyGame.NetworkManager.ConnectServer();
        };
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);
        MyGame.SpriteBatch.Begin();
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}