
public class PlayerDisconnectedEvent : Event
{
    public int playerDisconnectedId;

    public PlayerDisconnectedEvent(int _playerDisconnectedId) : base(EventTypes.ServerEvents.PLAYER_TEAM_CHANGE)
    {
        playerDisconnectedId = _playerDisconnectedId;
    }

    public PlayerDisconnectedEvent(int eventId, Packet _packet) : base((EventTypes.ServerEvents)eventId)
    {
        playerDisconnectedId = _packet.ReadInt();
    }

    public override void WriteToPacket(Packet _packet)
    {
        base.WriteToPacket(_packet);
        _packet.Write(playerDisconnectedId);
    }
}

