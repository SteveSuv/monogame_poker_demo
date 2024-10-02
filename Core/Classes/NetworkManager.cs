using Microsoft.Xna.Framework;

class NetworkManager
{
    private readonly PeerServer server = new();
    private readonly PeerCient client = new();
    public int port = 9000;
    public bool isHost = false;

    public void Update(GameTime gameTime)
    {
        server.Update();
        client.Update();
    }

    public void StartServer()
    {
        server.Start(port);
        isHost = true;
    }

    public void StoptServer()
    {
        server.Stop();
        isHost = false;
    }

    public void ConnectServer(string address = "localhost")
    {
        client.Connect(address: address, port: port);
    }

    public void SyncMessage(string message)
    {
        client.SendMessageToAll(message);
    }
}