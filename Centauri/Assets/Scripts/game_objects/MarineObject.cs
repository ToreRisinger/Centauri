
using System.Collections.Generic;
using UnityEngine;

public class MarineObject : CharacterObject
{
    public override List<Ability> GetAbilities()
    {
        return new List<Ability>();
    }
    public override void takeDamage()
    {
        AnimationEffectManager.instance.MarineBlood((Vector2)gameObject.transform.position + effectPoint);
    }
}

