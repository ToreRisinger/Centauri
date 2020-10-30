using System.Numerics;

public class StalkerObject : CharacterObject
{
    public StalkerObject(int _playerId, Vector2 _position, EObjectDirection _direction) : 
        base(_playerId, ECharacterType.STALKER, Stats.STALKER_HP, Stats.STALKER_REGEN, Stats.STALKER_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}



