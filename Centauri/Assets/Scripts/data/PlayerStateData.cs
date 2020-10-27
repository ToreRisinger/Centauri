using UnityEngine;

public class PlayerStateData
{
    public int id;
    public Vector2 position;
    public EObjectDirection direction;
    public ETeam teamId;

    public PlayerStateData(int _id, ETeam _teamId, Vector2 _position, EObjectDirection _direction)
    {
        id = _id;
        teamId = _teamId;
        position = _position;
        direction = _direction;
    }
}
