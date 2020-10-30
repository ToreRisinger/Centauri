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
            GameManager.instance.onPlayerJoin(_playerId, _username, (ETeam)_teamId);
        }
    }

    public static void GameState(Packet _packet)
    {
        GameState gameState = new GameState(_packet);

        GameManager.instance.pushGameState(gameState);
    }
}
