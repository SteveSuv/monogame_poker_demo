using LiteNetLib;
using Microsoft.Xna.Framework;

class NetworkManager
{
    public readonly PeerServer server = new();
    public readonly PeerCient client = new();
    public int port = 9000;
    public bool isHost = false;
    public List<NetPeer> ConnectedPeerList => client._serverPeer.NetManager.ConnectedPeerList;

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

    public void StopClient()
    {
        client.Stop();
    }

    public void SyncMessage(string message)
    {
        client.SendMessageToAll(message);
    }
}