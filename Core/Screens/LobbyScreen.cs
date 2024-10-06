
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;


class LobbyScreen(MyGame game) : GameScreen(game)
{
    private Node world = new()
    {
        transform = { localPosition = MyGame.ScreenCenter },

    };

    public override void LoadContent()
    {
        base.LoadContent();


        var computerName = Environment.MachineName;
        var ipv4Address = Dns.GetHostEntry(Dns.GetHostName())
                                 .AddressList
                                 .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                                 ?.ToString();

        world.AddChild(new LabelNode() { text = $"{computerName} 创建的房间  {ipv4Address}:9000", fontSize = 30, transform = { localPosition = new(0, -200) } });


        ////

        var _backButton = new ButtonNode()
        {
            transform = { localPosition = new(0, 10) },
            children = [
                new LabelNode() { text = "返回", transform = { color = Color.Black } }
            ]
        };


        _backButton.Click += (object sender, EventArgs e) =>
        {
            MyGame.NetworkManager.StoptServer();
            MyGame.NetworkManager.StopClient();
            MyGame.LoadScreen(new StartScreen(game));
        };

        world.AddChild(_backButton);



    }

    public override void Update(GameTime gameTime)
    {


        // var list = MyGame.NetworkManager.ConnectedPeerList;

        // for (int i = 0; i < list.Count; i++)
        // {
        //     var item = list[i];
        //     world.AddChild(new LabelNode() { text = $"PeerID: {item.Id}", fontSize = 30, transform = { localPosition = new(0, 100 + i * 20) } });
        // }
        

        // 应该更新

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