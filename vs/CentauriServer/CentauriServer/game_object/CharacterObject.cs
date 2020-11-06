
using System.Collections.Generic;
using System.Numerics;

public abstract class CharacterObject : HpObject
{
    public List<Ability> abilities;
    public ECharacterType characterType;
    public EObjectDirection direction;

    public int playerId;

    public int speed;

    public CharacterObject(int _playerId, ECharacterType _characterType, int _hp, int _regen, int _speed, ETeam _teamId, Vector2 _position, EObjectDirection _direction) : 
        base(_hp, _regen, _teamId, _position)
    {
        playerId = _playerId;
        direction = _direction;
        characterType = _characterType;
        speed = _speed;
        abilities = CreateAbilities();
    }

    public new void Update()
    {
        base.Update();

    }

    public List<Ability> GetAbilities()
    {
        return abilities;
    }

    public abstract List<Ability> CreateAbilities();
}
