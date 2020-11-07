using Server;
using System.Collections.Generic;
using System.Numerics;


public class RoachBite : Ability
{
    public RoachBite() : base(AbilityConstants.ROACH_BITE_NAME, AbilityConstants.ROACH_BITE_COOLDOWN)
    {

    }

    public override bool isEnabled()
    {
        return true;
    }

    public override bool run(CharacterObject obj, int turnNumber, Vector2 attackPoint, Vector2 direction)
    {
        if(true)
        {
            //TODO sanity check
            List<int> objectsHitIds = AbilityHelper.abilityAoeDamage(obj.id, obj.teamId, turnNumber, obj.position, attackPoint, direction, AbilityConstants.ROACH_BITE_RADIUS, AbilityConstants.ROACH_BITE_RANGE, AbilityConstants.ROACH_BITE_DAMAGE);
            if (objectsHitIds.Count > 0)
            {
                GameLogic.PushEvent(new DamageEvent(obj.playerId, objectsHitIds));
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}

