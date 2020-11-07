using System.Collections.Generic;
using System.Numerics;

public static class AbilityHelper
{
    public static List<int> abilityAoeDamage(int characterId, ETeam characterTeamId, Vector2 position, Vector2 attackPoint, Vector2 direction, float radius, float range, int damage)
    {
        Vector2 point = position + attackPoint + direction * range;
        List<CharacterObject> objectsHit = PhysicsUtil.findCharactersInRadius(characterId, point, radius);
        List<int> returnList = new List<int>();
        foreach (CharacterObject objectHit in objectsHit)
        {
            if (objectHit.teamId != characterTeamId)
            {
                objectHit.hp = objectHit.hp - damage;
                returnList.Add(objectHit.id);
            }
        }
        return returnList;
    }
}

