using System.Collections.Generic;
using System.Numerics;

class MarineObject : CharacterObject
{

    public MarineObject(int _playerId, Vector2 _position) : 
        base(_playerId, ECharacterType.MARINE, Stats.MARINE_HP, Stats.MARINE_REGEN, Stats.MARINE_SPEED, TeamManager.marineTeam.GetTeamId(), _position, EObjectDirection.TOP)
    {

    }

    public override List<Ability> CreateAbilities()
    {
        return new List<Ability>();
    }
}

