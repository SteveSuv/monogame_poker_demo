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

    public PeerServer()
    {
        serverListener.ConnectionRequestEvent += OnConnectionRequest;
        serverListener.PeerConnectedEvent += OnPeerConnected;
        serverListener.NetworkReceiveEvent += OnNetworkReceive;
        serverListener.PeerDisconnectedEvent += OnPeerDisconnected;

        server = new(serverListener) { AutoRecycle = true };

        // packetProcessor.SubscribeReusable<JoinPacket>(OnClientJoin);
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

    // private void OnClientJoin(JoinPacket packet)
    // {
    //     Console.WriteLine($"Server: OnClientJoin: {packet.username}");
    // }

    private void OnConnectionRequest(ConnectionRequest request)
    {
        Console.WriteLine($"Server: OnConnectionRequest");

        if (server.ConnectedPeersCount <= MaxConnectedPeersCount)
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
        // var peers = server.ConnectedPeerList.Select(x => new RoomPeer()
        // {
        //     address = x.Address.ToString(),
        //     addressFamily = x.AddressFamily,
        //     connectionState = x.ConnectionState,
        //     Id = x.Id,
        //     Mtu = x.Mtu,
        //     IsRunning = x.NetManager.IsRunning,
        //     Ping = x.Ping,
        //     Port = x.Port,
        //     RemoteId = x.RemoteId,
        //     RemoteTimeDelta = x.RemoteTimeDelta,
        //     RemoteUtcTime = x.RemoteUtcTime.ToString(),
        //     RoundTripTime = x.RoundTripTime,
        //     Tag = x.Tag.ToString(),
        //     TimeSinceLastPacket = x.TimeSinceLastPacket
        // }).ToList();

        var peers = server.ConnectedPeerList.Select(x => x.Id.ToString()).Reverse().ToArray();

        SendPacketToClients(new RoomStatePacket { Name = $"{Environment.UserName}创建的房间", Peers = peers }, DeliveryMethod.ReliableOrdered);
    }

    private void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Server: OnPeerConnected {peer.Id}");
        SyncRoomState();
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Server: OnPeerDisconnected {peer.Id}");
        SyncRoomState();
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Server: OnNetworkReceive {formPeer.Id}");
        serverPacketProcessor.ReadAllPackets(reader);
    }
}
