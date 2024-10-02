using LiteNetLib;
using LiteNetLib.Utils;

class PeerServer
{
    private readonly NetManager _server;
    private readonly EventBasedNetListener _serverListener;
    public static int MaxConnectedPeersCount = 4;

    public PeerServer()
    {
        _serverListener = new EventBasedNetListener();

        _server = new NetManager(_serverListener) { AutoRecycle = true };

        _serverListener.ConnectionRequestEvent += request =>
        {
            if (_server.ConnectedPeersCount < MaxConnectedPeersCount)
            {
                request.AcceptIfKey("test");
            }
            else
            {
                request.Reject();
            }

        };

        _serverListener.PeerConnectedEvent += clientPeer =>
        {
            Console.WriteLine($"Server: peer({clientPeer.Id}) connected");
        };

        _serverListener.PeerDisconnectedEvent += (clientPeer, disconnectInfo) =>
        {
            Console.WriteLine($"Server: peer({clientPeer.Id}) disconnected");
        };

        _serverListener.NetworkReceiveEvent += (fromPeer, dataReader, channelNumber, deliveryMethod) =>
        {
            var message = dataReader.GetString();
            Console.WriteLine($"Server: received data from {fromPeer.Id}: {message}");
            SendMessageToAll(message);
        };
    }

    public void Start(int port = 9000)
    {
        _server.Start(port); // 选择一个端口
        Console.WriteLine($"Server: server port {port} started. Waiting for connections...");
    }

    public void Stop()
    {
        _server.Stop();
    }

    public void Update()
    {
        _server.PollEvents();
    }

    public void SendMessageToAll(string message)
    {
        var writer = new NetDataWriter();
        writer.Put(message);
        _server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
    }
}
