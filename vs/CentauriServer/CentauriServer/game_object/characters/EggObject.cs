using System.Collections.Generic;
using System.Numerics;

public class EggObject : CharacterObject
{
    public EggObject(int _playerId, Vector2 _position) : 
        base(_playerId, ECharacterType.EGG, Stats.EGG_HP, Stats.EGG_REGEN, Stats.EGG_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, EObjectDirection.TOP)
    {

    }

    public override List<Ability> CreateAbilities()
    {
        return new List<Ability>();
    }
}

