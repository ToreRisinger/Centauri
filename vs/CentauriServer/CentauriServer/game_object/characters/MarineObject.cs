using System.Numerics;

class MarineObject : CharacterObject
{

    public MarineObject(Vector2 _position, EObjectDirection _direction) : 
        base(ECharacterType.MARINE, Stats.MARINE_HP, Stats.MARINE_REGEN, Stats.MARINE_SPEED, TeamManager.marineTeam.GetTeamId(), _position, _direction)
    {

    }
}

