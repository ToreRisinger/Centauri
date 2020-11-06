using System.Numerics;


public class AbilityActivationData
{
    public int abilityIndex;
    public Vector2 attackPoint;
    public Vector2 direction;

    public AbilityActivationData(int _abilityIndex, Vector2 _attackPoint, Vector2 _direction)
    {
        abilityIndex = _abilityIndex;
        attackPoint = _attackPoint;
        direction = _direction;
    }

    public AbilityActivationData(Packet _packet)
    {
        abilityIndex = _packet.ReadInt();
        attackPoint = _packet.ReadVector2();
        direction = _packet.ReadVector2();
    }

    public void WriteToPacket(Packet _packet)
    {
        _packet.Write(abilityIndex);
        _packet.Write(attackPoint);
        _packet.Write(direction);
    }
}

