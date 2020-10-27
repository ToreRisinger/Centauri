using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class Player
    {
        public int id;
        public string username;
        public ETeam teamId;

        public Vector2 position;
        public EObjectDirection direction;

        private Queue<PlayerCommandData> playerCommandQueue;
        private List<PlayerCommandData> playerCommandHistory;

        public Player(int _id, string _username, ETeam _teamId, Vector2 _spawnPosition)
        {
            id = _id;
            username = _username;
            teamId = _teamId;
            position = _spawnPosition;
            direction = EObjectDirection.TOP;

            playerCommandQueue = new Queue<PlayerCommandData>();
            playerCommandHistory = new List<PlayerCommandData>();
        }

        public void pushCommand(PlayerCommandData _command)
        {
            playerCommandQueue.Enqueue(_command);
        }

        public void Update()
        {
            while(playerCommandQueue.Count > 0)
            {

                //TODO add validation checks
                PlayerCommandData command = playerCommandQueue.Dequeue();
                position = command.position;
                direction = command.direction;

                playerCommandHistory.Add(command);
            }

            while(playerCommandHistory.Count > 10)
            {
                playerCommandHistory.RemoveAt(0);
            }
        }
    }
}
