using System;
using System.Collections.Generic;
using System.Numerics;

public class PlayerCommandData
{
    public int turnNumber;
    public float deltaTime;
    public EObjectDirection direction;
    public Vector2 position;
    public List<EPlayerAction> actions;
    public int playerId;
    public List<AbilityActivationData> abilityActivations;

    public PlayerCommandData(int _turnNumber, float _deltaTime, Vector2 _position, EObjectDirection _direction, List<EPlayerAction> _actions, List<AbilityActivationData> _abilityActivations)
    {
        turnNumber = _turnNumber;
        deltaTime = _deltaTime;
        position = _position;
        actions = _actions;
        direction = _direction;
        abilityActivations = _abilityActivations;
    }

    public PlayerCommandData(Packet _packet)
    {
        turnNumber = _packet.ReadInt();
        deltaTime = _packet.ReadFloat();
        position = _packet.ReadVector2();
        direction = (EObjectDirection)_packet.ReadInt();
        int actionCount = _packet.ReadInt();
        actions = new List<EPlayerAction>();
        for (int i = 0; i < actionCount; i++) {
            actions.Add((EPlayerAction)_packet.ReadInt());
        }
        int abilityActivationCount = _packet.ReadInt();
        abilityActivations = new List<AbilityActivationData>();
        for (int i = 0; i < abilityActivationCount; i++)
        {
            abilityActivations.Add(new AbilityActivationData(_packet));
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
        _packet.Write(abilityActivations.Count);
        foreach (AbilityActivationData abilityActivation in abilityActivations)
        {
            abilityActivation.WriteToPacket(_packet);
        }
    }
}

