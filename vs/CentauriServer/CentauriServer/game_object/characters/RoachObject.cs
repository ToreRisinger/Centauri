using System.Numerics;

public class RoachObject : CharacterObject
{
    public RoachObject(Vector2 _position, EObjectDirection _direction) : 
        base(ECharacterType.ROACH, Stats.ROACH_HP, Stats.ROACH_REGEN, Stats.ROACH_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}


