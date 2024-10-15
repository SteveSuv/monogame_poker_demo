using LiteNetLib.Utils;

class RoomStatePacket
{
    public string Name { get; set; }
    public RoomClient[] Clients { get; set; }
}

class RoomClientPacket
{
    public RoomClient Client { get; set; }
}

struct RoomClient : INetSerializable
{
    public string Name;
    public int PeerId;

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(Name ?? "");
        writer.Put(PeerId);
    }

    public void Deserialize(NetDataReader reader)
    {
        Name = reader.GetString();
        PeerId = reader.GetInt();
    }
}