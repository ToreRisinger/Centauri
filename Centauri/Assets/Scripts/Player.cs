
public class Player
{
    public int id;
    public string username;
    public ETeam teamId;
    public CharacterObject character;

    public Player(int _id, string _username, ETeam _teamId)
    {
        id = _id;
        username = _username;
        teamId = _teamId;
        character = null;
    }

}

