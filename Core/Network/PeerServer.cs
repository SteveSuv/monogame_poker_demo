using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;


class PeerServer
{
    public readonly NetManager server;
    private readonly EventBasedNetListener serverListener = new();
    private readonly NetDataWriter writer = new();
    private readonly NetPacketProcessor packetProcessor = new();

    public PeerServer()
    {
        serverListener.ConnectionRequestEvent += OnConnectionRequest;
        serverListener.PeerConnectedEvent += OnPeerConnected;
        serverListener.NetworkReceiveEvent += OnNetworkReceive;
        serverListener.PeerDisconnectedEvent += OnPeerDisconnected;

        server = new(serverListener) { AutoRecycle = true };

        packetProcessor.SubscribeReusable<JoinPacket>(OnClientJoin);
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

    public void SendPacketToClients<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
    {
        writer.Reset();
        packetProcessor.Write(writer, packet);
        server.SendToAll(writer, deliveryMethod);
    }

    private void OnClientJoin(JoinPacket packet)
    {
        Console.WriteLine($"Server: OnClientJoin: {packet.username}");
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
        SendPacketToClients(new JoinPacket { username = $"ID: {peer.Id}" }, DeliveryMethod.ReliableOrdered);
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Server: OnPeerDisconnected {peer.Id}");
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Server: OnNetworkReceive {formPeer.Id}");
        packetProcessor.ReadAllPackets(reader);
    }
}
