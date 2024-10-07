using LiteNetLib;
using LiteNetLib.Utils;
using Microsoft.Xna.Framework;


// public class PlayerData
// {
//     public int Id;
//     public string Name;
//     public Vector2 Position;

//     public PlayerData() { } // 默认构造函数用于反序列化

//     public PlayerData(int id, string name, Vector2 position)
//     {
//         Id = id;
//         Name = name;
//         Position = position;
//     }
// }

class Peer
{
    public readonly NetManager netManager;
    private EventBasedNetListener _netListener;

    private NetPacketProcessor _netPacketProcessor;
    private NetSerializer _netSerializer;

    // private List<PlayerData> _players;

    public Peer()
    {
        // _players = new List<PlayerData>();
        _netListener = new EventBasedNetListener();

        _netListener.PeerConnectedEvent += OnPeerConnected;
        _netListener.NetworkReceiveEvent += OnNetworkReceive;
        _netListener.PeerDisconnectedEvent += OnPeerDisconnected;
        _netListener.ConnectionRequestEvent += OnConnectionRequest;

        netManager = new NetManager(_netListener) { AutoRecycle = true };

        _netPacketProcessor = new NetPacketProcessor();
        _netSerializer = new NetSerializer();

        // 注册消息处理
        // _netPacketProcessor.SubscribeReusable<PlayerData>(OnPlayerDataReceived);
        // _netSerializer.Register<PlayerData>();
    }


    public void Update(GameTime gameTime)
    {
        netManager.PollEvents();
        // _packetProcessor.ReadAllPackets(_netManager);
    }


    private void OnConnectionRequest(ConnectionRequest request)
    {
        Console.WriteLine($"OnConnectionRequest");

        if (netManager.ConnectedPeersCount < 4)
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
        Console.WriteLine($"OnPeerConnected {peer.Id}");
    }

    private void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"OnPeerDisconnected {peer.Id}");
    }

    private void OnNetworkReceive(NetPeer formPeer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
    {
        Console.WriteLine($"OnNetworkReceive {formPeer.Id}");
    }

    // public void SendPlayerData(NetPeer peer, PlayerData playerData)
    // {
    //     var writer = new NetDataWriter();
    //     _netSerializer.Serialize(writer, playerData);
    //     peer.Send(writer, DeliveryMethod.ReliableOrdered);
    // }

    // private void OnPlayerDataReceived(NetPeer peer, PlayerData playerData)
    // {
    //     // 更新玩家列表
    //     _players.Add(playerData);
    //     // 处理玩家数据（如更新 UI）
    // }
}