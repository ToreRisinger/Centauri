

using System.Numerics;

public class PlayerJoinedEvent : Event
{
    public int playerId;
    public string username;
    public int teamId;
    public Vector2 spawnPosition;

    public PlayerJoinedEvent(int _playerId, string _username, int _teamId, Vector2 _spawnPosition) : base(EventTypes.ServerEvents.PLAYER_JOINED)
    {
        playerId = _playerId;
        username = _username;
        teamId = _teamId;
        spawnPosition = _spawnPosition;
    }

    public PlayerJoinedEvent(int eventId, Packet _packet) : base((EventTypes.ServerEvents)eventId)
    {
        playerId = _packet.ReadInt();
        username = _packet.ReadString();
        teamId = _packet.ReadInt();
        spawnPosition = _packet.ReadVector2();
    }

    public override void WriteToPacket(Packet _packet)
    {
        base.WriteToPacket(_packet);
        _packet.Write(playerId);
        _packet.Write(username);
        _packet.Write(teamId);
        _packet.Write(spawnPosition);
    }
}

