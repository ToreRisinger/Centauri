using System.Collections.Generic;
using UnityEngine;


public class PhysicsUtil
{
    private static float characterHitBoxRadius = 2.0f;


    public static List<CharacterObject> findCharactersInRadius(Vector2 point, float radius)
    {
        List<CharacterObject> charactersHit = new List<CharacterObject>();
        foreach(CharacterObject obj in DataProvider.getCharacters().Values)
        {
            Vector2 position = obj.transform.position;
            float diffLength = (position - point).magnitude;
            if(diffLength <= radius + characterHitBoxRadius)
            {
                charactersHit.Add(obj);
            }
        }
       
        return charactersHit;
    }

    public static List<CharacterObject> findCharactersInRadius(int excludeCharacterId, System.Numerics.Vector2 point, float radius)
    {
        List<CharacterObject> charactersHit = new List<CharacterObject>();
        foreach (CharacterObject obj in GameManager.characters.Values)
        {
            Vector2 position = obj.transform.position;
            float diffLength = (position - new Vector2(point.X, point.Y)).magnitude;
            if (diffLength <= radius + characterHitBoxRadius && obj.id != excludeCharacterId)
            {
                charactersHit.Add(obj);
            }
        }
        
        return charactersHit;
    }
}

