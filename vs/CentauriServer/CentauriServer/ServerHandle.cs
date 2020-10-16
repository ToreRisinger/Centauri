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
            int _turnNumber = _packet.ReadInt();
            float _deltaTime = _packet.ReadFloat();
            Vector2 _position = _packet.ReadVector2();
            int _direction = _packet.ReadInt();
            int nrOfActions = _packet.ReadInt();
            HashSet<EPlayerAction> _actions = new HashSet<EPlayerAction>();
            for (int i = 0; i < nrOfActions; i++)
            {
                _actions.Add((EPlayerAction)_packet.ReadInt());
            }
            PlayerCommandData cmd = new PlayerCommandData(_turnNumber, _deltaTime, _position, (EPlayerDirection) _direction, _actions);
            //GameServer.clients[_fromClient].player.pushCommand(cmd);
            GameLogic.onPlayerCommand(_fromClient, cmd);
        }
    }
}
