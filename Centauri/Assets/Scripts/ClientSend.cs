using UnityEngine;
using static Shared.PacketTypes;
using PlayerDirection;
using System.Collections.Generic;

public class ClientSend : MonoBehaviour
{
   private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);

    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets

    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            _packet.Write(UIManager.instance.usernameField.text);

            SendTCPData(_packet);
        }
    }

    public static void PlayerCommand(HashSet<EPlayerAction> _actions, Vector2 _position, EPlayerDirection direction, int turnNumber, float deltaTime)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerCommand))
        {
            _packet.Write(turnNumber);
            _packet.Write(deltaTime);
            _packet.Write(new System.Numerics.Vector2(_position.x, _position.y));
            _packet.Write((int)direction);
            _packet.Write(_actions.Count);
            foreach (EPlayerAction action in _actions)
            {
                _packet.Write((int)action);
            }
            SendTCPData(_packet);
        }
    }

    /*
    public static void PlayerInput(bool[] _inputs)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerInput))
        {
            _packet.Write(_inputs.Length);
            foreach(bool _input in _inputs)
            {
                _packet.Write(_input);
            }

            
            SendTCPData(_packet);
        }
    }
    */


    #endregion
}
