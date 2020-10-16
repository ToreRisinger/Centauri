using PlayerDirection;
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
        for(int i = 0; i < playerCount; i++)
        {
            int _id = _packet.ReadInt();
            string _username = _packet.ReadString();
            Vector2 _position = _packet.ReadVector2();
            GameManager.instance.SpawnPlayer(_id, _username, _position);
        }
    }

    public static void SpawnPlayer(Packet _packet)
    {
        int _id = _packet.ReadInt();
        string _username = _packet.ReadString();
        Vector2 _position = _packet.ReadVector2();

        GameManager.instance.SpawnPlayer(_id, _username, _position);
    }

    public static void GameState(Packet _packet)
    {
        int turnNumber = _packet.ReadInt();
        int nrOfPlayers = _packet.ReadInt();

        List<PlayerStateData> playerStates = new List<PlayerStateData>();
        for(int i = 0; i < nrOfPlayers; i++)
        {
            int _id = _packet.ReadInt();
            Vector2 _position = _packet.ReadVector2();
            EPlayerDirection direction = (EPlayerDirection)_packet.ReadInt();
            playerStates.Add(new PlayerStateData(_id, _position, direction));
        }

        GameManager.instance.pushGameState(new GameState(turnNumber, playerStates));
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
