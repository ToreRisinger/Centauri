using System.Numerics;

public class CharacterStateData
{
    public int id;
    public int playerId;
    public ECharacterType type;
    public Vector2 position;
    public EObjectDirection direction;
    public ETeam teamId;
    public int hp;
    public int speed;

    public CharacterStateData(int _id, int _playerId, ECharacterType _type, ETeam _teamId, Vector2 _position, EObjectDirection _direction, int _hp, int _speed)
    {
        id = _id;
        playerId = _playerId;
        type = _type;
        teamId = _teamId;
        position = _position;
        direction = _direction;
        hp = _hp;
        speed = _speed;
    }

    public CharacterStateData(Packet _packet)
    {
        id = _packet.ReadInt();
        playerId = _packet.ReadInt();
        type = (ECharacterType)_packet.ReadInt();
        position = _packet.ReadVector2();
        direction = (EObjectDirection)_packet.ReadInt();
        teamId = (ETeam)_packet.ReadInt();
        hp = _packet.ReadInt();
        speed = _packet.ReadInt();
    }

    public void WriteToPacket(Packet _packet)
    {
        _packet.Write(id);
        _packet.Write(playerId);
        _packet.Write((int)type);
        _packet.Write(new System.Numerics.Vector2(position.X, position.Y));
        _packet.Write((int)direction);
        _packet.Write((int)teamId);
        _packet.Write(hp);
        _packet.Write(speed);
    }
}
