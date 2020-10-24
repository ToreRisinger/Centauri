using System.Numerics;

class GameObject
{
    public int id;
    public ETeam teamId;
    public Vector2 position;

    public GameObject(int _id, ETeam _teamId, Vector2 _position)
    {
        id = _id;
        teamId = _teamId;
        position = _position;
    }
}

