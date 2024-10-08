using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;

class PeerCient
{
    public readonly NetManager client;
    private readonly EventBasedNetListener clientListener = new();
    private NetPeer serverPeer;
    private readonly NetDataWriter writer = new();
    private readonly NetPacketProcessor packetProcessor = new();

    public RoomStatePacket roomState;

    public PeerCient()
    {
        clientListener.PeerConnectedEvent += OnPeerConnected;
        clientListener.NetworkReceiveEvent += OnNetworkReceive;
        clientListener.PeerDisconnectedEvent += OnPeerDisconnected;

        client = new NetManager(clientListener) { AutoRecycle = true };

        packetProcessor.SubscribeReusable<RoomStatePacket>(OnRoomStateChange);
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

    public void Stop()
    {
        client.Stop();
    }

    public void SendPacketToServer<T>(T packet, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered) where T : class, new()
    {
        writer.Reset();
        packetProcessor.Write(writer, packet);
        serverPeer.Send(writer, deliveryMethod);
    }

    private void OnRoomStateChange(RoomStatePacket packet)
    {
        Console.WriteLine(packet.Peers);
        Console.WriteLine($"Client: OnClientJoin: {packet?.Peers?.Length ?? 0}");
        roomState = packet;
    }

    private void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Client: OnPeerConnected {peer.Id}");
        serverPeer = peer;
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Client: OnPeerDisconnected {peer.Id}");
        serverPeer = null;
        roomState.Peers = [];
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Client: OnNetworkReceive {formPeer.Id}");
        packetProcessor.ReadAllPackets(reader);
    }
}
