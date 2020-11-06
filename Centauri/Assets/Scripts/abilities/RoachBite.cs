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
        Vector2 abilityPosition = obj.GetAttackPoint() + (Vector2)obj.transform.position + obj.GetAttackDirection() * AbilityConstants.ROACH_BITE_RANGE;
        
        if (obj.IsLocalCharacter())
        {
           
        } 
        else
        {

        }

        playAnimation(abilityPosition);
    }

    public void playAnimation(Vector2 position)
    {
        AnimationEffectManager.instance.RoachBite(position);
    }
}

