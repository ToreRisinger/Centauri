
public class PlayerTeamChangeEvent : Event
{
    public int fromTeamId;
    public int toTeamId;
    public int playerId;

    public PlayerTeamChangeEvent(int _playerId, int _fromTeamId, int _toTeamId) : base(EventTypes.ServerEvents.PLAYER_TEAM_CHANGE)
    {
        playerId = _playerId;
        fromTeamId = _fromTeamId;
        toTeamId = _toTeamId;
    }

    public PlayerTeamChangeEvent(int eventId, Packet _packet) : base((EventTypes.ServerEvents)eventId)
    {
        playerId = _packet.ReadInt();
        fromTeamId = _packet.ReadInt();
        toTeamId = _packet.ReadInt();
    }

    public override void WriteToPacket(Packet _packet)
    {
        base.WriteToPacket(_packet);
        _packet.Write(playerId);
        _packet.Write(fromTeamId);
        _packet.Write(toTeamId);
    }

    public override string GetName()
    {
        return $"PlayerTeamChangeEvent (playerId: {playerId}, fromTeamId: {fromTeamId}, toTeamId: {toTeamId})";
    }
}


