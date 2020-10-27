using System.Numerics;

public class QueenObject : CharacterObject
{
    public QueenObject(Vector2 _position, EObjectDirection _direction) : 
        base(ECharacterType.QUEEN, Stats.QUEEN_HP, Stats.QUEEN_REGEN, Stats.QUEEN_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}
