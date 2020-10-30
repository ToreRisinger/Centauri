using System.Collections.Generic;
using System.Linq;
using static PacketTypes;

namespace Server
{
    class ServerSend
    {
        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for(int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].tcp.SendData(_packet);
            }
        }

        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for( int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if(i != _exceptClient)
                {
                    GameServer.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            GameServer.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= GameServer.MaxPlayers; i++)
            {
                GameServer.clients[i].udp.SendData(_packet);
            }
        }

        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i < GameServer.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    GameServer.clients[i].udp.SendData(_packet);
                }
            }
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            GameServer.clients[_toClient].udp.SendData(_packet);
        }

        #region Packets

        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);
                SendTCPData(_toClient, _packet);
            }
        }

        public static void Initialize(int _toClient, int _mapId, List<Player> _players)
        {
            using (Packet _packet = new Packet((int)ServerPackets.initialize))
            {
                _packet.Write(_mapId);
                _packet.Write(_players.Count);
                foreach(Player _player in _players)
                {
                    if(_player != null)
                    {
                        _packet.Write(_player.id);
                        _packet.Write(_player.username);
                        _packet.Write((int)_player.teamId);
                    }
                    
                }
                SendTCPData(_toClient, _packet);
            }
        }

        public static void GameState(GameState gameState)
        {
            using (Packet _packet = new Packet((int)ServerPackets.gameState))
            {
                gameState.WriteToPacket(_packet);
                SendTCPDataToAll(_packet);
            }
        }

        #endregion
    }
}
