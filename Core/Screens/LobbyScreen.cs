
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

    private Label _title => new($"{computerName}创建的房间  {ipv4Address}:9000") { };
    private readonly Button _backButton = new() { label = new("返回"), position = new(MyGame.ScreenWidth, 0), origin = Origin.Center };


    public override void LoadContent()
    {
        base.LoadContent();
        _backButton.position.X = MyGame.ScreenWidth - _backButton.Size.X / 2 - 20;
        _backButton.position.Y = _backButton.Size.Y / 2;
        _title.position.Y = _backButton.Size.Y / 2;
        _title.position.X = _title.position.X + 20;

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
        _title.Update(gameTime);
        _backButton.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        MyGame.GraphicsDevice.Clear(Color.Black);
        MyGame.SpriteBatch.Begin();
        _title.Draw(gameTime);
        _backButton.Draw(gameTime);
        MyGame.SpriteBatch.End();
    }
}