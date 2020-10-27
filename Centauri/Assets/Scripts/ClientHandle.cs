using System;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void Initialize(Packet _packet)
    {
        int mapId = _packet.ReadInt();
        GameManager.instance.Initialize(mapId);
        int playerCount = _packet.ReadInt();
        for (int i = 0; i < playerCount; i++)
        {
            int _playerId = _packet.ReadInt();
            string _username = _packet.ReadString();
            int _teamId = _packet.ReadInt();
            Vector2 _position = _packet.ReadUnityVector2();
            GameManager.instance.onPlayerJoin(_playerId, _username, (ETeam)_teamId, _position);
        }
    }

    public static void GameState(Packet _packet)
    {
        //TurnNumber
        int turnNumber = _packet.ReadInt();

        //Player states
        int nrOfPlayers = _packet.ReadInt();
        List<PlayerStateData> playerStates = new List<PlayerStateData>();
        for (int i = 0; i < nrOfPlayers; i++)
        {
            int _id = _packet.ReadInt();
            int _teamId = _packet.ReadInt();
            Vector2 _position = _packet.ReadUnityVector2();
            EObjectDirection direction = (EObjectDirection)_packet.ReadInt();
            playerStates.Add(new PlayerStateData(_id, (ETeam)_teamId, _position, direction));
        }

        //Events
        Queue<Event> events = new Queue<Event>();
        int nrOfEvents = _packet.ReadInt();
        for (int i = 0; i < nrOfEvents; i++)
        {
            events.Enqueue(ReadEventFromPacket(_packet));
        }

        GameManager.instance.pushGameState(new GameState(turnNumber, playerStates, events));
    }

    private static Dictionary<int, Func<int, Packet, Event>> eventMap = new Dictionary<int, Func<int, Packet, Event>>
    {
        {(int)EventTypes.ServerEvents.PLAYER_DISCONNECTED, (eventId, packet) => { return new PlayerDisconnectedEvent(eventId, packet); } },
        {(int)EventTypes.ServerEvents.PLAYER_JOINED, (eventId, packet) => { return new PlayerJoinedEvent(eventId, packet); } },
        {(int)EventTypes.ServerEvents.PLAYER_TEAM_CHANGE, (eventId, packet) => { return new PlayerTeamChangeEvent(eventId, packet); } }

    };

    private static Event ReadEventFromPacket(Packet _packet)
    {
        int eventId = _packet.ReadInt();
        return eventMap[eventId](eventId, _packet);
    }

    /*
    public static void PlayerState(Packet _packet)
    {
        int _id = _packet.ReadInt();
        Vector2 _position = _packet.ReadVector2();
        if(GameManager.players.ContainsKey(_id))
        {
             GameManager.instance.MovePlayer(_id, _position);
        }
    }
    */
}
