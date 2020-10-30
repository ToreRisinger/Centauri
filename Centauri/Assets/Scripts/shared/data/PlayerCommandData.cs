using System;
using System.Collections.Generic;
using System.Numerics;

public class PlayerCommandData
{
    public int turnNumber;
    public float deltaTime;
    public EObjectDirection direction;
    public Vector2 position;
    public HashSet<EPlayerAction> actions;

    public PlayerCommandData(int _turnNumber, float _deltaTime, Vector2 _position, EObjectDirection _direction, HashSet<EPlayerAction> _actions)
    {
        turnNumber = _turnNumber;
        deltaTime = _deltaTime;
        position = _position;
        actions = _actions;
        direction = _direction;
    }

    public PlayerCommandData(Packet _packet)
    {
        turnNumber = _packet.ReadInt();
        deltaTime = _packet.ReadFloat();
        position = _packet.ReadVector2();
        direction = (EObjectDirection)_packet.ReadInt();
        int actionCount = _packet.ReadInt();
        actions = new HashSet<EPlayerAction>();
        for (int i = 0; i < actionCount; i++) {
            actions.Add((EPlayerAction)_packet.ReadInt());
        }
    }

    public void WriteToPacket(Packet _packet)
    {
        _packet.Write(turnNumber);
        _packet.Write(deltaTime);
        _packet.Write(new System.Numerics.Vector2(position.X, position.Y));
        _packet.Write((int)direction);
        _packet.Write(actions.Count);
        foreach (EPlayerAction action in actions)
        {
            _packet.Write((int)action);
        }
    }
}

