

using System.Numerics;

public class HpObject : GameObject
{
    public int hp;
    public int regen;

    public HpObject(int _hp, int _regen, ETeam _teamId, Vector2 _position) : base(_teamId, _position)
    {
        hp = _hp;
        regen = _regen;
    }

    public new void Update()
    {
        //TODO calculate regen based on delta time
    }
}

