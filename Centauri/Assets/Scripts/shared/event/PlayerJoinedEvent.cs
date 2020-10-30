

using System.Numerics;

public class PlayerJoinedEvent : Event
{
    public int playerId;
    public string username;
    public ETeam teamId;

    public PlayerJoinedEvent(int _playerId, string _username, ETeam _teamId) : base(EventTypes.ServerEvents.PLAYER_JOINED)
    {
        playerId = _playerId;
        username = _username;
        teamId = _teamId;
    }

    public PlayerJoinedEvent(int eventId, Packet _packet) : base((EventTypes.ServerEvents)eventId)
    {
        playerId = _packet.ReadInt();
        username = _packet.ReadString();
        teamId = (ETeam)_packet.ReadInt();
    }

    public override void WriteToPacket(Packet _packet)
    {
        base.WriteToPacket(_packet);
        _packet.Write(playerId);
        _packet.Write(username);
        _packet.Write((int)teamId);
    }

    public override string GetName()
    {
        return $"PlayerJoinedEvent (playerId: {playerId}, username: {username}, teamId: {teamId})";
    }
}

