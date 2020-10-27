using System.Collections.Generic;
using System.Numerics;

public class PlayerCommandData
{
    public int turnNumber;
    public float deltaTime;
    public EObjectDirection direction;
    public Vector2 position;
    HashSet<EPlayerAction> actions;

    public PlayerCommandData(int _turnNumber, float _deltaTime, Vector2 _position, EObjectDirection _direction, HashSet<EPlayerAction> _actions)
    {
        turnNumber = _turnNumber;
        deltaTime = _deltaTime;
        position = _position;
        actions = _actions;
        direction = _direction;
    }
}

