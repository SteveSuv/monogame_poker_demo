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

        _createServerButton.OnClick += (object sender, Vector2 mousePos) =>
        {
            MyGame.Peer.peerServer.Start(9000);
            MyGame.Peer.peerClient.Connect();
            MyGame.LoadScreen(new LobbyScreen(game));
        };

        world.AddChild(_createServerButton);


        var _connectServerButton = new ButtonNode()
        {

            Transform = { localPosition = new(0, 160) },
            children = [
                new LabelNode() { text = "加入房间", Transform = { color = Color.Black } },
            ]
        };

        _connectServerButton.OnClick += (object sender, Vector2 mousePos) =>
        {
            MyGame.Peer.peerClient.Connect();
            MyGame.LoadScreen(new LobbyScreen(game));
        };


        world.AddChild(_connectServerButton);

        var _nameInput = new InputNode() { Transform = { localPosition = new(0, -200) } };

        _nameInput.OnInput += (object sender, string text) =>
        {
            Console.WriteLine(text);
        };

        world.AddChild(_nameInput);

        world.AddChild(new ModalNode());
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