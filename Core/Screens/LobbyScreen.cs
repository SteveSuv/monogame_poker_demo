using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Screens;

class LobbyScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new() { localPosition = MyGame.ScreenCenter };
    private static RoomStatePacket RoomState => MyGame.Peer.peerClient.roomState;

    public override void Initialize()
    {
        world.NodeManager.AddChild(new LabelNode() { tag = "Name", fontSize = 30, localPosition = new(0, -200) });
        world.NodeManager.AddChild(new Node() { tag = "Clients" });

        var bg = new SpriteNode() { texture = Assets.TextureBackground, WorldPosition = new(0, 0), LayerDepth = 0 };
        world.Children.Insert(0, bg);

        var btn = new ButtonNode() { localPosition = new(-100, 10) };
        btn.NodeManager.AddChild(new LabelNode() { text = "返回", color = Color.Black });
        btn.ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnClick = (object sender, Vector2 mousePos) =>
                    {
                        MyGame.Peer.Stop();
                        MyGame.LoadScreen(new StartScreen(game));
                    }
        });
        world.NodeManager.AddChild(btn);


        var btn2 = new ButtonNode() { localPosition = new(100, 10) };
        btn2.NodeManager.AddChild(new LabelNode() { text = "开始", color = Color.Black });
        btn2.ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnClick = (object sender, Vector2 mousePos) =>
                    {
                        MyGame.LoadScreen(new RoomScreen(game));
                    }
        });
        world.NodeManager.AddChild(btn2);

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {

        if (RoomState != null)
        {
            var nameNode = world.NodeManager.GetChildByTag<LabelNode>("Name");
            nameNode.text = RoomState.Name;

            var clientsNode = world.NodeManager.GetChildByTag<Node>("Clients");
            clientsNode.NodeManager.RemoveAllChildren();

            var clients = RoomState.Clients ?? [];

            if (clients.Length == 0)
            {
                MyGame.LoadScreen(new StartScreen(game));
            }

            for (int i = 0; i < clients.Length; i++)
            {
                var client = clients[i];
                var isAdmin = client.PeerId == 0;

                clientsNode.NodeManager.AddChild(new LabelNode() { text = $"玩家编号{client.PeerId}: {client.Name} {(isAdmin ? "(房主)" : "")}", fontSize = 30, localPosition = new(0, 100 + i * 40) });
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
        MyGame.GraphicsDevice.Clear(MyGame.ThemeColor);
        MyGame.SpriteBatch.Begin(samplerState: SamplerState.PointClamp);
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}