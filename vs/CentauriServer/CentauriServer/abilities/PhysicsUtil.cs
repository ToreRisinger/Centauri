using Server;
using System.Collections.Generic;
using System.Numerics;


public class PhysicsUtil
{
    private static float characterHitBoxRadius = 0.40f;


    public static List<CharacterObject> findCharactersInRadius(Vector2 point, float radius)
    {
        //TODO need to sync collision box for each type of gameobject with client
        List<CharacterObject> charactersHit = new List<CharacterObject>();
        foreach(CharacterObject obj in GameLogic.characters.Values)
        {
            Vector2 position = obj.position;
            float diffLength = (position - point).Length();
            if(diffLength <= radius + characterHitBoxRadius)
            {
                charactersHit.Add(obj);
            }
        }
       
        return charactersHit;
    }

    public static List<CharacterObject> findCharactersInRadius(int excludeCharacterId, Vector2 point, float radius)
    {
        List<CharacterObject> charactersHit = new List<CharacterObject>();
        foreach (CharacterObject obj in GameLogic.characters.Values)
        {
            Vector2 position = obj.position;
            float diffLength = (position - point).Length();
            if (diffLength <= radius + characterHitBoxRadius && obj.id != excludeCharacterId)
            {
                charactersHit.Add(obj);
            }
        }
        
        return charactersHit;
    }
}

