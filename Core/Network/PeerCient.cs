using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;

class PeerCient
{
    public readonly NetManager client;
    private readonly EventBasedNetListener clientListener = new();
    private readonly NetDataWriter clientWriter = new();
    private readonly NetPacketProcessor clientPacketProcessor = new();
    private NetPeer serverPeer;

    public RoomStatePacket roomState = null;

    public PeerCient()
    {
        clientListener.PeerConnectedEvent += OnPeerConnected;
        clientListener.NetworkReceiveEvent += OnNetworkReceive;
        clientListener.PeerDisconnectedEvent += OnPeerDisconnected;

        client = new(clientListener) { AutoRecycle = true };

        clientPacketProcessor.RegisterNestedType<RoomClientPacket>(() => new());
        clientPacketProcessor.RegisterNestedType<RoomStatePacket>(() => new());
        clientPacketProcessor.SubscribeReusable<RoomStatePacket>(OnRoomStateChange);
    }

    private void OnRoomStateChange(RoomStatePacket packet)
    {
        Console.WriteLine($"Client: OnRoomStateChange: {packet.Name}");
        roomState = packet;
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
        clientWriter.Reset();
        clientPacketProcessor.Write(clientWriter, packet);
        serverPeer.Send(clientWriter, deliveryMethod);
    }

    private void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Client: OnPeerConnected {peer.Id}");
        serverPeer = peer;
        Console.WriteLine($"Name {Environment.UserName}, RemoteId {client.FirstPeer.RemoteId}");
        SendPacketToServer(new RoomClientPacket()
        {
            Name = Environment.UserName,
            PeerId = client.FirstPeer.RemoteId
        });
    }


    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Client: OnPeerDisconnected {peer.Id}");
        serverPeer = null;
        roomState = null;
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"Client: OnNetworkReceive {formPeer.Id}");
        clientPacketProcessor.ReadAllPackets(reader);
    }
}
