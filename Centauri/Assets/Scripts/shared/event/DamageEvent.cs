using System;
using System.Collections.Generic;
using System.Text;


public class DamageEvent : Event
{
    public int playerCausedDamageId;
    public List<int> objectsHitIds;

    public DamageEvent(int _playerCausedDamageId, List<int> _objectsHitIds) : base(EventTypes.ServerEvents.DAMAGE_EVENT)
    {
        playerCausedDamageId = _playerCausedDamageId;
        objectsHitIds = _objectsHitIds;
    }

    public DamageEvent(int eventId, Packet _packet) : base((EventTypes.ServerEvents)eventId)
    {
        playerCausedDamageId = _packet.ReadInt();
        int numberOfPlayersHit = _packet.ReadInt();
        objectsHitIds = new List<int>();
        for (int i = 0; i < numberOfPlayersHit; i++)
        {
            objectsHitIds.Add(_packet.ReadInt());
        }
    }

    public override string GetName()
    {
        return $"DamageEvent (playerCausedDamageId: {playerCausedDamageId})";
    }

    public override void WriteToPacket(Packet _packet)
    {
        base.WriteToPacket(_packet);
        _packet.Write(playerCausedDamageId);
        _packet.Write(objectsHitIds.Count);
        foreach(int id in objectsHitIds)
        {
            _packet.Write(id);
        }
    }
}

