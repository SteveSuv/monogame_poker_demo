using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class LobbyScreen(MyGame game) : GameScreen(game)
{
    private static RoomStatePacket RoomState => MyGame.Peer.peerClient.roomState;

    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
        children = [
            new LabelNode() { tag = "Name", fontSize = 30, localPosition = new(0, -200) },
            new Node() { tag = "Clients" },
        ]
    };

    public override void LoadContent()
    {
        base.LoadContent();


        // var computerName = Environment.MachineName;
        // var ipv4Address = Dns.GetHostEntry(Dns.GetHostName())
        //                          .AddressList
        //                          .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
        //                          ?.ToString();

        // world.AddChild(new LabelNode() { text = $"{computerName} 创建的房间  {ipv4Address}:9000", fontSize = 30,  localPosition = new(0, -200) } });
        ////

        var _backButton = new ButtonNode()
        {
            localPosition = new(0, 10),
            children = [
                new LabelNode() { text = "返回", color = Color.Black }
            ]
        };


        _backButton.OnClick += (object sender, Vector2 mousePos) =>
        {
            MyGame.Peer.Stop();
            MyGame.LoadScreen(new StartScreen(game));
        };

        world.AddChild(_backButton);

        // world.AddChild(new Node() { tag = "Clients" });
    }

    public override void Update(GameTime gameTime)
    {

        if (RoomState != null)
        {
            var nameNode = world.GetChildByTag<LabelNode>("Name");
            nameNode.text = RoomState.Name;

            var clientsNode = world.GetChildByTag<Node>("Clients");
            clientsNode.RemoveAllChildren();

            var list = RoomState.Peers;

            if (list.Length == 0)
            {
                MyGame.LoadScreen(new StartScreen(game));
            }

            for (int i = 0; i < list.Length; i++)
            {
                var item = list[i];
                clientsNode.AddChild(new LabelNode() { text = $"PeerID: ID_{item}", fontSize = 30, localPosition = new(0, 100 + i * 40) });
            }
        }
        else
        {
            MyGame.LoadScreen(new StartScreen(game));
        }


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