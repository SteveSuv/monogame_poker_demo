using LiteNetLib.Utils;

class RoomStatePacket
{
    public string Name { get; set; }
    public RoomClientPacket[] Clients { get; set; }
}



class RoomClientPacket : INetSerializable
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