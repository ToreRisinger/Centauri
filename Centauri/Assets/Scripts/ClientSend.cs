using UnityEngine;
using static PacketTypes;
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

    public static void PlayerCommand(PlayerCommandData data)
    {
        using (Packet _packet = new Packet((int)ClientPackets.playerCommand))
        {
            data.WriteToPacket(_packet);
            SendTCPData(_packet);
        }
    }

    #endregion
}
