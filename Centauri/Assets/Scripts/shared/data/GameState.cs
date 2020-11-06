using System;
using System.Collections.Generic;

public class GameState
{
    public int turnNumber;
    public List<CharacterStateData> characters;
    public List<BuildingStateData> buildings;
    public Queue<Event> events;

    public GameState(int _turnNumber, List<CharacterStateData> _characters, List<BuildingStateData> _buildings, Queue<Event> _events)
    {
        turnNumber = _turnNumber;
        characters = _characters;
        buildings = _buildings;
        events = _events;
    }
    public GameState(Packet _packet)
    {
        turnNumber = _packet.ReadInt();
        int characterCount = _packet.ReadInt();

        characters = new List<CharacterStateData>();
        for(int i = 0; i < characterCount; i++)
        {
            characters.Add(new CharacterStateData(_packet));
        }

        int buildingsCount = _packet.ReadInt();
        buildings = new List<BuildingStateData>();
        for (int i = 0; i < buildingsCount; i++)
        {
            buildings.Add(new BuildingStateData(_packet));
        }

        events = new Queue<Event>();
        int eventCount = _packet.ReadInt();
        for (int i = 0; i < eventCount; i++)
        {
            events.Enqueue(ReadEventFromPacket(_packet));
        }
    }

    public void WriteToPacket(Packet _packet)
    {
        _packet.Write(turnNumber);
        _packet.Write(characters.Count);
        foreach (CharacterStateData character in characters)
        {
            character.WriteToPacket(_packet);
        }

        _packet.Write(buildings.Count);
        foreach (BuildingStateData building in buildings)
        {
            building.WriteToPacket(_packet);
        }

        _packet.Write(events.Count);
        Queue<Event> tmpEvents = new Queue<Event>(events);
        for (int i = 0; i < events.Count; i++)
        {
            tmpEvents.Dequeue().WriteToPacket(_packet);
        }

    }

    private static Dictionary<int, Func<int, Packet, Event>> eventMap = new Dictionary<int, Func<int, Packet, Event>>
    {
        {(int)EventTypes.ServerEvents.PLAYER_DISCONNECTED, (eventId, packet) => { return new PlayerDisconnectedEvent(eventId, packet); } },
        {(int)EventTypes.ServerEvents.PLAYER_JOINED, (eventId, packet) => { return new PlayerJoinedEvent(eventId, packet); } },
        {(int)EventTypes.ServerEvents.PLAYER_TEAM_CHANGE, (eventId, packet) => { return new PlayerTeamChangeEvent(eventId, packet); } },
        {(int)EventTypes.ServerEvents.DAMAGE_EVENT, (eventId, packet) => { return new DamageEvent(eventId, packet); } },
        {(int)EventTypes.ServerEvents.PLAYER_ACTIVATE_ABILITY, (eventId, packet) => { return new PlayerActivateAbilityEvent(eventId, packet); } }
    };

    private static Event ReadEventFromPacket(Packet _packet)
    {
        int eventId = _packet.ReadInt();
        return eventMap[eventId](eventId, _packet);
    }
}

