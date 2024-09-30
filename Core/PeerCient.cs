using LiteNetLib;
using LiteNetLib.Utils;

class PeerCient
{
    private readonly NetManager _client;
    private readonly EventBasedNetListener _clientListener;
    private NetPeer _serverPeer;


    public PeerCient()
    {
        _clientListener = new EventBasedNetListener();

        _client = new NetManager(_clientListener) { AutoRecycle = true };

        _clientListener.PeerConnectedEvent += serverPeer =>
        {
            Console.WriteLine($"Client: connected to server({serverPeer.Id})");
            _serverPeer = serverPeer;
        };

        _clientListener.PeerDisconnectedEvent += (serverPeer, disconnectInfo) =>
        {
            Console.WriteLine($"Client: disconnected from server({serverPeer.Id})");
        };

        _clientListener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
        {
            var message = dataReader.GetString();
            Console.WriteLine($"Client: received data from {fromPeer.Id}: {message}");
        };
    }

    public void Connect(string address = "localhost", int port = 9000, string key = "test")
    {
        _client.Start();
        _client.Connect(address, port, key);
        Console.WriteLine($"Client: connecting to {address}:{port}");
    }

    public void Stop()
    {
        _client.Stop();
    }

    public void Update()
    {
        _client.PollEvents();
    }

    public void SendMessageToAll(string message)
    {
        if (_serverPeer != null)
        {
            var writer = new NetDataWriter();
            writer.Put(message);
            _serverPeer.Send(writer, DeliveryMethod.ReliableOrdered);
        }
    }
}
