using System.Numerics;

public class EggObject : CharacterObject
{
    public EggObject(Vector2 _position) : base(ECharacterType.EGG, Stats.EGG_HP, Stats.EGG_REGEN, Stats.EGG_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, EObjectDirection.TOP)
    {

    }
}

