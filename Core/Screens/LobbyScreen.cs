
using System.Net;
using System.Net.Sockets;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Screens;


class LobbyScreen(MyGame game) : GameScreen(game)
{
    private readonly string computerName = Environment.MachineName;

    // 获取IPv4地址
    private readonly string ipv4Address = Dns.GetHostEntry(Dns.GetHostName())
                             .AddressList
                             .FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                             ?.ToString();

    private Node world = new()
    {
        transform = { localPosition = MyGame.ScreenCenter },
        children = [
            // {computerName}创建的房间  {ipv4Address}:9000
                new LabelNode() { text = $"房间", fontSize = 40 },
            new ButtonNode()
            {
                tag = "Back",
                transform = { localPosition = new(0, 100) },
                children = [
                    new LabelNode() { text = "返回", transform = { color = Color.Black } }
                ]
            }
                ]
    };

    private ButtonNode _backButton => world.GetChildByTag<ButtonNode>("Back");

    public override void LoadContent()
    {
        base.LoadContent();

        _backButton.Click += (object sender, EventArgs e) =>
        {
            MyGame.NetworkManager.StoptServer();
            MyGame.NetworkManager.StopClient();
            MyGame.LoadScreen(new StartScreen(game));
        };

        foreach (var item in MyGame.NetworkManager.ConnectedPeerList)
        {
            Console.WriteLine(item.Id);
        }
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