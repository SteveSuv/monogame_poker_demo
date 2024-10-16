using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new()
    {
        localPosition = MyGame.ScreenCenter,
        Children = [
            new SpriteNode() { texture = Assets.TextureLogo, localPosition = new(0, -100), scale = new(0.2f), rotation = 0 },
            new LabelNode() { text = "CheatPoker", fontSize = 40 },
        ]
    };
    public override void Initialize()
    {
        var bg = new SpriteNode() { texture = Assets.TextureBackground, WorldPosition = new(0, 0), LayerDepth = 0 };
        world.Children.Insert(0, bg);

        world.NodeManager.AddChild(new ButtonNode()
        {
            localPosition = new(0, 100),
            Children = [
                new LabelNode() { text = "创建房间", color = Color.Black },
            ]
        }).ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnClick = (object sender, Vector2 mousePos) =>
                    {
                        MyGame.Peer.peerServer.Start(9000);
                        MyGame.Peer.peerClient.Connect();
                        MyGame.LoadScreen(new LobbyScreen(game));
                    }
        });

        world.NodeManager.AddChild(new ButtonNode()
        {

            localPosition = new(0, 160),
            Children = [
                new LabelNode() { text = "加入房间", color = Color.Black },
            ]
        }).ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnClick = (object sender, Vector2 mousePos) =>
                    {
                        MyGame.Peer.peerClient.Connect();
                        MyGame.LoadScreen(new LobbyScreen(game));
                    }
        });

        world.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        world.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(MyGame.ThemeColor);
        MyGame.SpriteBatch.Begin();
        world.Draw();
        MyGame.SpriteBatch.End();
    }
}