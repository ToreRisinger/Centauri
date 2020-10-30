
using System.Numerics;

class TyrantObject : CharacterObject
{
    public TyrantObject(int _playerId, Vector2 _position, EObjectDirection _direction) : 
        base(_playerId, ECharacterType.TYRANT, Stats.TYRANT_HP, Stats.TYRANT_REGEN, Stats.TYRANT_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}



