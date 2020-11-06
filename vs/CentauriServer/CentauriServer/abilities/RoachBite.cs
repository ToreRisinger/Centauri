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

    public override bool run(CharacterObject obj, Vector2 attackPoint, Vector2 direction)
    {
        if(true)
        {
            //TODO sanity check
            Vector2 point = obj.position + attackPoint + direction * AbilityConstants.ROACH_BITE_RANGE;
            List<CharacterObject> objectsHit = PhysicsUtil.findCharactersInRadius(obj.id, point, AbilityConstants.ROACH_BITE_RADIUS);
            List<int> playersHitId = new List<int>();
            foreach (CharacterObject objectHit in objectsHit)
            {
                if(objectHit.teamId != obj.teamId)
                {
                    objectHit.hp = objectHit.hp - AbilityConstants.ROACH_BITE_DAMAGE;
                    playersHitId.Add(objectHit.playerId);
                }
            }

            if(playersHitId.Count > 0)
            {
                GameLogic.PushEvent(new DamageEvent(obj.playerId, playersHitId));
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}

