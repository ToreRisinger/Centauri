
public abstract class Event
{
    public EventTypes.ServerEvents eventType;

    public Event(EventTypes.ServerEvents _eventType)
    {
        eventType = _eventType;
    }

    public Event(Packet _packet)
    {
        eventType = (EventTypes.ServerEvents)_packet.ReadInt();
    }

    public virtual void WriteToPacket(Packet _packet)
    {
        _packet.Write((int)eventType);
    }
}
