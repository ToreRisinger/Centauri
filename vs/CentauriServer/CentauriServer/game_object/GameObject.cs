using System.Numerics;

public class GameObject
{
    public static int idCounter = 0;

    public int id;
    public ETeam teamId;
    public Vector2 position;

    public GameObject(ETeam _teamId, Vector2 _position)
    {
        id = idCounter++;
        teamId = _teamId;
        position = _position;
    }

    public void Update()
    {

    }
}

