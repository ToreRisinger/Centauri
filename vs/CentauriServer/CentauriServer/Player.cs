using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class Player
    {
        public int id;
        public string username;

        public Vector2 position;
        public EPlayerDirection direction;

        private Queue<PlayerCommandData> playerCommandQueue;
        private List<PlayerCommandData> playerCommandHistory;

        public Player(int _id, string _username, Vector2 _spawnPosition)
        {
            id = _id;
            username = _username;
            position = _spawnPosition;
            direction = EPlayerDirection.TOP;

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
