using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;

class StartScreen(MyGame game) : GameScreen(game)
{
    private readonly Node world = new() { localPosition = MyGame.ScreenCenter };
    public override void Initialize()
    {
        world.NodeManager.AddChild(new SpriteNode() { texture = Assets.TextureLogo, localPosition = new(0, -100), scale = new(0.2f), rotation = 0 });
        world.NodeManager.AddChild(new LabelNode() { text = "CheatPoker", fontSize = 40 });

        var bg = new SpriteNode() { texture = Assets.TextureBackground, WorldPosition = new(0, 0), LayerDepth = 0 };
        world.Children.Insert(0, bg);

        var btn = new ButtonNode() { localPosition = new(0, 100) };
        btn.NodeManager.AddChild(new LabelNode() { text = "创建房间", color = Color.Black });
        btn.ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnClick = (object sender, Vector2 mousePos) =>
                    {
                        MyGame.Peer.peerServer.Start(9000);
                        MyGame.Peer.peerClient.Connect();
                        MyGame.LoadScreen(new LobbyScreen(game));
                    }
        });
        world.NodeManager.AddChild(btn);

        var btn2 = new ButtonNode() { localPosition = new(0, 160) };
        btn2.NodeManager.AddChild(new LabelNode() { text = "加入房间", color = Color.Black });
        btn2.ComponentManager.AddComponent(new MouseEventComponent()
        {
            OnClick = (object sender, Vector2 mousePos) =>
                    {
                        MyGame.Peer.peerClient.Connect();
                        MyGame.LoadScreen(new LobbyScreen(game));
                    }
        });
        world.NodeManager.AddChild(btn2);

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