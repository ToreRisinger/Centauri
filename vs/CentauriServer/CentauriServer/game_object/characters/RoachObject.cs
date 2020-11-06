using System.Collections.Generic;
using System.Numerics;

public class RoachObject : CharacterObject
{
    public RoachObject(int _playerId, Vector2 _position) : 
        base(_playerId, ECharacterType.ROACH, Stats.ROACH_HP, Stats.ROACH_REGEN, Stats.ROACH_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, EObjectDirection.TOP)
    {

    }

    public override List<Ability> CreateAbilities()
    {
        return new List<Ability>()
        {
            new RoachBite()
        };
    }
}


