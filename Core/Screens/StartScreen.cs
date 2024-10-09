using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
        children = [
            new SpriteNode() { tag = "Logo", texture = Assets.TextureLogo, localPosition = new(0, -100), scale = new(0.2f) },
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

        var modalNode = new ModalNode() { layerDepth = -1 };
        var inputNode = new InputNode() { text = "localhost:9000" };
        var buttonNode = new ButtonNode() { localPosition = new(0, 80), children = [new LabelNode() { text = "连接", color = Color.Black }] };

        buttonNode.OnClick += (object sender, Vector2 mousePos) =>
        {
            var arr = inputNode.text.Split(":");
            var address = arr[0].Trim();
            var port = int.Parse(arr[1].Trim());
            MyGame.Peer.peerClient.Connect(address, port);
            MyGame.LoadScreen(new LobbyScreen(game));
        };

        modalNode.AddChild(inputNode).AddChild(buttonNode);
        world.AddChild(modalNode);

        _connectServerButton.OnClick += (object sender, Vector2 mousePos) =>
        {
            modalNode.layerDepth = 1;
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

        // var _nameInput = new InputNode() { localPosition = new(0, -200) };

        // _nameInput.OnInput += (object sender, string text) =>
        // {
        //     Console.WriteLine(text);
        // };

        // world.AddChild(_nameInput);
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        // var logo = world.GetChildByTag<SpriteNode>("Logo");

        MyGame.GraphicsDevice.Clear(Color.Black);

        MyGame.SpriteBatch.Begin();

        // Assets.EffectGaussianBlur.Parameters["TexelSize"].SetValue(new Vector2(1) / logo.Size);
        // Assets.EffectGaussianBlur.CurrentTechnique.Passes[0].Apply();

        world.Draw();

        MyGame.SpriteBatch.End();
    }
}