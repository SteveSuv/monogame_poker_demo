using LiteNetLib;
using Microsoft.Xna.Framework;

class PeerCient
{
    public readonly NetManager client;
    public readonly EventBasedNetListener clientListener;
    public NetPeer serverPeer;

    public PeerCient()
    {
        clientListener = new EventBasedNetListener();

        clientListener.PeerConnectedEvent += OnPeerConnected;
        clientListener.NetworkReceiveEvent += OnNetworkReceive;
        clientListener.PeerDisconnectedEvent += OnPeerDisconnected;

        client = new NetManager(clientListener) { AutoRecycle = true };
    }

    public void Update(GameTime gameTime)
    {
        client.PollEvents();
    }

    public void Connect(string address = "localhost", int port = 9000, string key = "")
    {
        client.Start();
        client.Connect(address, port, key);
        Console.WriteLine($"Client: connecting to {address}:{port}");
    }

    private void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Client: OnPeerConnected {peer.Id}");
        serverPeer = peer;
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Client: OnPeerDisconnected {peer.Id}");
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Client: OnNetworkReceive {formPeer.Id}");
    }
}
