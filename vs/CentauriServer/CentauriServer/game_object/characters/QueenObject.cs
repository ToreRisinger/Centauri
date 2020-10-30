using System.Numerics;

public class QueenObject : CharacterObject
{
    public QueenObject(int _playerId, Vector2 _position, EObjectDirection _direction) : 
        base(_playerId, ECharacterType.QUEEN, Stats.QUEEN_HP, Stats.QUEEN_REGEN, Stats.QUEEN_SPEED, TeamManager.centauriTeam.GetTeamId(), _position, _direction)
    {

    }
}
