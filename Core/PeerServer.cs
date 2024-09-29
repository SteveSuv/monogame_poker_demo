using LiteNetLib;
using LiteNetLib.Utils;

class PeerServer
{
    private NetManager _server;
    private EventBasedNetListener _serverListener;

    public PeerServer()
    {
        _serverListener = new EventBasedNetListener();

        _server = new NetManager(_serverListener) { AutoRecycle = true };

        _serverListener.ConnectionRequestEvent += request =>
        {
            if (_server.ConnectedPeersCount < 4)
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
            Console.WriteLine("Server: Peer connected: " + clientPeer.Id);
        };

        _serverListener.PeerDisconnectedEvent += (clientPeer, disconnectInfo) =>
        {
            Console.WriteLine("Server: Peer disconnected: " + clientPeer.Id);
        };

        _serverListener.NetworkReceiveEvent += (fromPeer, dataReader, channelNumber, deliveryMethod) =>
        {
            var message = dataReader.GetString();
            Console.WriteLine($"Server: Received data from {fromPeer.Id}: {dataReader.GetString()}");
            SendMessageToAll(message);
        };
    }

    public void Start(int port = 9000)
    {
        _server.Start(port); // 选择一个端口
        Console.WriteLine("Server: Server started. Waiting for connections...");
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
