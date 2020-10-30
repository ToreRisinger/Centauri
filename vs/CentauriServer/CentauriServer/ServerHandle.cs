using Server;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{GameServer.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if(_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }

            GameLogic.PlayerJoined(_fromClient, _username);
        }

        public static void PlayerCommand(int _fromClient, Packet _packet)
        {
            PlayerCommandData cmd = new PlayerCommandData(_packet);
            GameLogic.onPlayerCommand(_fromClient, cmd);
        }
    }
}
