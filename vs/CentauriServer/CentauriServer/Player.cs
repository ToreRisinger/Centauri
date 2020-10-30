using System.Collections.Generic;
using System.Numerics;

namespace Server
{
    class Player
    {
        public int id;
        public string username;
        public ETeam teamId;

        public CharacterObject gameObj;

        public Player(int _id, string _username, ETeam _teamId)
        {
            id = _id;
            username = _username;
            teamId = _teamId;
            gameObj = null;
        }

        public override string ToString()
        {
            return $"[PLAYER: (id: {id}, username: {username}, teamId: {teamId})]";
        }
    }
}
