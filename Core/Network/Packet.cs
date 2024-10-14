using LiteNetLib.Utils;

class RoomStatePacket : INetSerializable
{
    public string Name = "";
    public RoomClientPacket[] Clients = [];

    public void Serialize(NetDataWriter writer)
    {
        writer.Put(Name);
        writer.PutArray(Clients, 2);
    }

    public void Deserialize(NetDataReader reader)
    {
        Name = reader.GetString();
        Clients = reader.GetArray<RoomClientPacket>(2);
    }
}



class RoomClientPacket : INetSerializable
{
    public string Name = "";
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