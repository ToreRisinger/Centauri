using System.Numerics;

public class AlphaObject : CharacterObject
{
    public AlphaObject(int _playerId, Vector2 _position, EObjectDirection _direction) : 
        base(_playerId, ECharacterType.ALPHA, Stats.ALPHA_HP, Stats.ALPHA_REGEN, Stats.ALPHA_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}

