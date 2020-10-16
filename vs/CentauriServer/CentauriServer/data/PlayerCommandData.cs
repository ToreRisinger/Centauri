using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

class PlayerCommandData
{
    public int turnNumber;
    public float deltaTime;
    public EPlayerDirection direction;
    public Vector2 position;
    HashSet<EPlayerAction> actions;

    public PlayerCommandData(int _turnNumber, float _deltaTime, Vector2 _position, EPlayerDirection _direction, HashSet<EPlayerAction> _actions)
    {
        turnNumber = _turnNumber;
        deltaTime = _deltaTime;
        position = _position;
        actions = _actions;
        direction = _direction;
    }
}

