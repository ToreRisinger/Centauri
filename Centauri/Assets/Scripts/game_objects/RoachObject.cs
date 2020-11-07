
using System.Collections.Generic;

public class RoachObject : CharacterObject
{
    private static List<Ability> abilities = new List<Ability>()
    {
        new RoachBite(),
    };

    public override List<Ability> GetAbilities()
    {
        return abilities;
    }

    public override void takeDamage()
    {
        AnimationEffectManager.instance.MarineBlood(gameObject.transform.position);
    }
}

