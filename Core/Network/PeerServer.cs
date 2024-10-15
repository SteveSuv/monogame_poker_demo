using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;


class PeerServer
{
    public readonly NetManager server;
    private readonly EventBasedNetListener serverListener = new();
    private readonly NetDataWriter serverWriter = new();
    private readonly NetPacketProcessor serverPacketProcessor = new();
    public int MaxConnectedPeersCount = 2;

    public Dictionary<int, RoomClient> clients = [];

    public PeerServer()
    {
        serverListener.ConnectionRequestEvent += OnConnectionRequest;
        serverListener.PeerConnectedEvent += OnPeerConnected;
        serverListener.NetworkReceiveEvent += OnNetworkReceive;
        serverListener.PeerDisconnectedEvent += OnPeerDisconnected;

        server = new(serverListener) { AutoRecycle = true };

        serverPacketProcessor.RegisterNestedType<RoomClient>();
        serverPacketProcessor.SubscribeReusable<RoomClientPacket>(OnClientStateChange);
    }

    private void OnClientStateChange(RoomClientPacket packet)
    {
        Console.WriteLine($"Server: OnClientStateChange: {packet.Client.Name} {packet.Client.PeerId}");
        clients.TryAdd(packet.Client.PeerId, packet.Client);
        SyncRoomState();
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

    public void Stop()
    {
        server.Stop();
        SyncRoomState();
    }

    public void SendPacketToClients<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
    {
        serverWriter.Reset();
        serverPacketProcessor.Write(serverWriter, packet);
        server.SendToAll(serverWriter, deliveryMethod);
    }

    private void OnConnectionRequest(ConnectionRequest request)
    {
        Console.WriteLine($"Server: OnConnectionRequest");

        if (server.ConnectedPeersCount < MaxConnectedPeersCount)
        {
            request.AcceptIfKey("");
        }
        else
        {
            request.Reject();
        }
    }

    private void SyncRoomState()
    {
        var Clients = clients.Values.ToArray();
        SendPacketToClients(new RoomStatePacket { Name = $"{Environment.UserName}创建的房间", Clients = Clients });
    }

    private void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Server: OnPeerConnected {peer.Id}");
        SyncRoomState();
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Server: OnPeerDisconnected {peer.Id}");
        clients.Remove(peer.Id);
        SyncRoomState();
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Server: OnNetworkReceive {formPeer.Id}");
        serverPacketProcessor.ReadAllPackets(reader);
    }
}
