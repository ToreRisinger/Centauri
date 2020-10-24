
using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class GameLogic
    {
        private static Map map = new TestMap();
        private static Dictionary<int, Player> players = new Dictionary<int, Player>();

        private static Queue<Event> events = new Queue<Event>();

        private static TeamManager teamManager = new TeamManager();

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
            ServerSend.GameState(new List<Player>(players.Values), events, turnNumber);
        }

        public static void PlayerJoined(int _playerId, string _playerName)
        {
            Vector2 _newPlayerPosition;
            int _teamId;

            //Add player to a team
            if (teamManager.marineTeam.GetNrOfPlayers() > teamManager.centauriTeam.GetNrOfPlayers())
            {
                _newPlayerPosition = Map.mapPosition + new Vector2(map.getAlienSpawnPoint().X, map.getAlienSpawnPoint().Y);
                teamManager.centauriTeam.AddPlayer(_playerId);
                _teamId = teamManager.centauriTeam.GetTeamId();
            }
            else
            {
                _newPlayerPosition = Map.mapPosition + new Vector2(map.getMarineSpawnPoint().X, map.getMarineSpawnPoint().Y);
                teamManager.marineTeam.AddPlayer(_playerId);
                _teamId = teamManager.marineTeam.GetTeamId();
            }

            Player newPlayer = new Player(_playerId, _playerName, _teamId, _newPlayerPosition);
            players.Add(_playerId, newPlayer);

            //Send init data to joined player (map, all players etc)
            ServerSend.Initialize(_playerId, map.GetMapId(), new List<Player>(players.Values));

            //Send player joined event to all players
            PushEvent(new PlayerJoinedEvent(_playerId, _playerName, _teamId, _newPlayerPosition));
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
                Player player = players[playerLeftId];
                players.Remove(player.id);
                teamManager.getTeam(player.teamId).RemovePlayer(playerLeftId);
            }
        }

        private static void PushEvent(Event evnt) 
        {
            events.Enqueue(evnt);
        }
    }
}
