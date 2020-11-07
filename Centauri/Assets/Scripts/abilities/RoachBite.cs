using System.Collections.Generic;
using UnityEngine;

public class RoachBite : Ability
{
    public RoachBite() : base(AbilityConstants.ROACH_BITE_NAME, AbilityConstants.ROACH_BITE_COOLDOWN)
    {

    }

    public override bool isEnabled()
    {
        return true;
    }

    protected override void runInternal(CharacterObject obj)
    {
        System.Numerics.Vector2 attackPoint = new System.Numerics.Vector2(obj.GetAttackPoint().x, obj.GetAttackPoint().y);
        System.Numerics.Vector2 direction = new System.Numerics.Vector2(obj.GetAttackDirection().x, obj.GetAttackDirection().y);
        System.Numerics.Vector2 position = new System.Numerics.Vector2(obj.transform.position.x, obj.transform.position.y);
        List<int> objectsHitIds = AbilityHelper.abilityAoeDamage(obj.id, obj.teamId, position, attackPoint, direction, AbilityConstants.ROACH_BITE_RADIUS, AbilityConstants.ROACH_BITE_RANGE, AbilityConstants.ROACH_BITE_DAMAGE);


        if (obj.IsLocalCharacter())
        {
           foreach(int id in objectsHitIds)
           {
                GameManager.characters[id].takeDamage();
           }
        }
        else
        {

        }
        
        playAnimation((Vector2)obj.transform.position + obj.GetAttackPoint() + obj.GetAttackDirection() * AbilityConstants.ROACH_BITE_RANGE);
    }

    public void playAnimation(Vector2 position)
    {
        AnimationEffectManager.instance.RoachBite(position);
    }
}

