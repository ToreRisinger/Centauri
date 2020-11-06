
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
}

