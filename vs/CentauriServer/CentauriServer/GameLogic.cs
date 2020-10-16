
using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class GameLogic
    {
        private static Map map = new TestMap();
        private static Dictionary<int, Player> players = new Dictionary<int, Player>();

        private static int turnNumber = 0;
        public static void Update()
        {
            GameLogic.turnNumber++;

            ThreadManager.UpdateMain();

            foreach (Player _player in players.Values)
            {
                //Update their position based on input
                _player.Update();

                //If player shoots, check what turn number it was and look for collisions on that turn
            }

            //Compile a gamestate package which is sent to all players
            //Send game state to all, embedd the turn number
            ServerSend.GameState(new List<Player>(players.Values), turnNumber);
        }

        public static void PlayerJoined(int _playerId, string _playerName)
        {
            Vector2 newPlayerPosition = Map.mapPosition + new Vector2(map.getMarineSpawnPoint().X, map.getMarineSpawnPoint().Y);
            Player newPlayer = new Player(_playerId, _playerName, newPlayerPosition);
            players.Add(_playerId, newPlayer);

            //Send all ingame players to the new client
            ServerSend.Initialize(_playerId, map.GetMapId(), new List<Player>(players.Values));

            //Send new client player to all ingame players
            foreach (Player _player in players.Values)
            {
                ServerSend.SpawnPlayer(_player.id, newPlayer);
            }
        }

        public static void onPlayerCommand(int playerId, PlayerCommandData cmd)
        {
            if(players.ContainsKey(playerId))
            {
                players[playerId].pushCommand(cmd);
            }
        }

        public static void onPlayerLeft(int playerLeftId)
        {
            if (players.ContainsKey(playerLeftId))
            {
                players.Remove(playerLeftId);
            }
        }
    }
}
