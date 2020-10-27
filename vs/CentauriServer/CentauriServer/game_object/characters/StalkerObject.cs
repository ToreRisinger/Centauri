using System.Numerics;

public class StalkerObject : CharacterObject
{
    public StalkerObject(Vector2 _position, EObjectDirection _direction) : 
        base(ECharacterType.STALKER, Stats.STALKER_HP, Stats.STALKER_REGEN, Stats.STALKER_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}



