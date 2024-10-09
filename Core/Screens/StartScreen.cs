using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{

    private readonly ModalNode modalNode = new() { layerDepth = -1 };
    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
        children = [
            new SpriteNode() { texture = Assets.TextureLogo, localPosition = new(0, -100), scale = new(0.2f) },
            new LabelNode() { text = "二十一点", fontSize = 40 },
        ]
    };
    public override void LoadContent()
    {
        base.LoadContent();

        var _createServerButton = new ButtonNode()
        {

            localPosition = new(0, 100),
            children = [
                new LabelNode() { text = "创建房间", color = Color.Black },
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

            localPosition = new(0, 160),
            children = [
                new LabelNode() { text = "加入房间", color = Color.Black },
            ]
        };

        _connectServerButton.OnClick += (object sender, Vector2 mousePos) =>
        {
            MyGame.Peer.peerClient.Connect();
            MyGame.LoadScreen(new LobbyScreen(game));
        };

        // world.AddChild(modalNode);

        // _connectServerButton.OnMouseEnter += (object sender, Vector2 mousePos) =>
        // {
        //     modalNode.layerDepth = 1;
        // };

        // _connectServerButton.OnMouseLeave += (object sender, Vector2 mousePos) =>
        // {
        //     modalNode.layerDepth = -1;
        // };

        // _connectServerButton.OnMouseMove += (object sender, Vector2 mousePos) =>
        // {
        //     modalNode.WorldPosition = mousePos;
        //     modalNode.origin = Origin.TopLeft;
        // };

        world.AddChild(_connectServerButton);

        var _nameInput = new InputNode() { localPosition = new(0, -200) };

        _nameInput.OnInput += (object sender, string text) =>
        {
            Console.WriteLine(text);
        };

        world.AddChild(_nameInput);
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