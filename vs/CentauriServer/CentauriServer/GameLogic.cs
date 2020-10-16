
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Server
{
    class GameLogic
    {
        private static Map map = new TestMap();

        private static int turnNumber = 0;
        public static void Update()
        {
            GameLogic.turnNumber++;

            ThreadManager.UpdateMain();

            List<Player> players = new List<Player>();
            foreach (Client _client in GameServer.clients.Values)
            {
                if(_client.player != null)
                {
                    //Update their position based on input
                    _client.player.Update();
                    
                    players.Add(_client.player);

                    //If player shoots, check what turn number it was and look for collisions on that turn
                }
            }

            //Compile a gamestate package which is sent to all players
            //Send game state to all, embedd the turn number
            ServerSend.GameState(players, turnNumber);
        }

        public static void PlayerJoined(int _playerId, string _playerName)
        {
            Client newClient = GameServer.clients[_playerId];
            Vector2 newPlayerPosition = Map.mapPosition + new Vector2(map.getMarineSpawnPoint().X, map.getMarineSpawnPoint().Y);
            newClient.createPlayer(_playerName, newPlayerPosition);

            List<Player> players = new List<Player>();
            foreach(Client _client in GameServer.clients.Values)
            {
                if(_client.player != null)
                {
                    players.Add(_client.player);
                }
            }


            //Send all ingame players to the new client
            ServerSend.Initialize(_playerId, map.GetMapId(), players);


            //Send new client player to all ingame players
            foreach (Client _client in GameServer.clients.Values)
            {
                if (_client.player != null)
                {
                    ServerSend.SpawnPlayer(_client.id, newClient.player);
                }
            }
        }
    }
}
