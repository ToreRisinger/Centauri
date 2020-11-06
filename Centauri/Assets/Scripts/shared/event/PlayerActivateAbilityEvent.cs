
public class PlayerActivateAbilityEvent : Event
{
    public int playerId;
    public int abilityIndex;

    public PlayerActivateAbilityEvent(int _playerId, int _abilityIndex) : base(EventTypes.ServerEvents.PLAYER_ACTIVATE_ABILITY)
    {
        playerId = _playerId;
        abilityIndex = _abilityIndex;
    }

    public PlayerActivateAbilityEvent(int eventId, Packet _packet) : base((EventTypes.ServerEvents)eventId)
    {
        playerId = _packet.ReadInt();
        abilityIndex = _packet.ReadInt();
    }

    public override string GetName()
    {
        return $"PlayerActivateAbilityEvent (playerId: {playerId}, abilityIndex: {abilityIndex})";
    }

    public override void WriteToPacket(Packet _packet)
    {
        base.WriteToPacket(_packet);
        _packet.Write(playerId);
        _packet.Write(abilityIndex);
    }
}

