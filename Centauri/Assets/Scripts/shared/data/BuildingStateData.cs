

using System.Numerics;

public class BuildingStateData
{
    public int id;
    public EBuildingType type;
    public Vector2 position;
    public ETeam teamId;
    public int hp;

    public BuildingStateData(int _id, EBuildingType _type, ETeam _teamId, Vector2 _position, int _hp)
    {
        id = _id;
        type = _type;
        teamId = _teamId;
        position = _position;
        hp = _hp;
    }

    public BuildingStateData(Packet _packet)
    {
        id = _packet.ReadInt();
        type = (EBuildingType)_packet.ReadInt();
        position = _packet.ReadVector2();
        teamId = (ETeam)_packet.ReadInt();
        hp = _packet.ReadInt();
    }

    public void WriteToPacket(Packet _packet)
    {
        _packet.Write(id);
        _packet.Write((int)type);
        _packet.Write(new System.Numerics.Vector2(position.X, position.Y));
        _packet.Write((int)teamId);
        _packet.Write(hp);
    }
}

