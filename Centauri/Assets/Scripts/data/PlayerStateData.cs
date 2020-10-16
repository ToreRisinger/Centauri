using PlayerDirection;
using UnityEngine;

public class PlayerStateData
{
    public int id;
    public Vector2 position;
    public EPlayerDirection direction;

    public PlayerStateData(int _id, Vector2 _position, EPlayerDirection _direction)
    {
        id = _id;
        position = _position;
        direction = _direction;
    }
}
