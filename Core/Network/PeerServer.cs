using LiteNetLib;
using Microsoft.Xna.Framework;

class PeerServer
{
    public readonly NetManager server;
    public readonly EventBasedNetListener serverListener;

    public PeerServer()
    {
        serverListener = new EventBasedNetListener();

        serverListener.ConnectionRequestEvent += OnConnectionRequest;
        serverListener.PeerConnectedEvent += OnPeerConnected;
        serverListener.NetworkReceiveEvent += OnNetworkReceive;
        serverListener.PeerDisconnectedEvent += OnPeerDisconnected;

        server = new NetManager(serverListener) { AutoRecycle = true };
    }

    public void Update(GameTime gameTime)
    {
        server.PollEvents();
    }

    public void Start(int port = 9000)
    {
        server.Start(port); // 选择一个端口
        Console.WriteLine($"Server: server port {port} started. Waiting for connections...");
    }

    private void OnConnectionRequest(ConnectionRequest request)
    {
        Console.WriteLine($"Server: OnConnectionRequest");

        if (server.ConnectedPeersCount < 4)
        {
            request.AcceptIfKey("");
        }
        else
        {
            request.Reject();
        }

    }

    private void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Server: OnPeerConnected {peer.Id}");
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Server: OnPeerDisconnected {peer.Id}");
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Server: OnNetworkReceive {formPeer.Id}");
    }
}
