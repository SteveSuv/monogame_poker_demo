using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new()
    {
        Transform = { localPosition = MyGame.ScreenCenter },
        children = [
            new SpriteNode() { texture = Assets.TextureLogo, Transform = { localPosition = new(0, -100), scale = new(0.2f) } },
            new LabelNode() { text = "二十一点", fontSize = 40 },
        ]
    };
    public override void LoadContent()
    {
        base.LoadContent();


        var _createServerButton = new ButtonNode()
        {

            Transform = { localPosition = new(0, 100) },
            children = [
                new LabelNode() { text = "创建房间", Transform = { color = Color.Black } },
            ]
        };

        _createServerButton.Click += (object sender, EventArgs e) =>
        {
            MyGame.Peer.peerServer.Start(9000);
            MyGame.Peer.peerClient.Connect();
            MyGame.LoadScreen(new LobbyScreen(game));
        };


        var _connectServerButton = new ButtonNode()
        {

            Transform = { localPosition = new(0, 160) },
            children = [
                new LabelNode() { text = "加入房间", Transform = { color = Color.Black } },
            ]
        };

        _connectServerButton.Click += (object sender, EventArgs e) =>
        {
            MyGame.Peer.peerClient.Connect();
            MyGame.LoadScreen(new LobbyScreen(game));
        };

        world.AddChild(_createServerButton);
        world.AddChild(_connectServerButton);
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