using Server;
using System.Collections.Generic;
using System.Numerics;


public class PhysicsUtil
{
    private static float characterHitBoxRadius = 2.0f;


    public static List<CharacterObject> findCharactersInRadius(int turnNumber, Vector2 point, float radius)
    {
        List<CharacterObject> charactersHit = new List<CharacterObject>();
        foreach (CharacterStateData obj in GameLogic.gameStates[turnNumber].characters)
        {
            Vector2 position = obj.position;
            float diffLength = (position - point).Length();
            if (diffLength <= radius + characterHitBoxRadius)
            {
                if (GameLogic.characters.ContainsKey(obj.id))
                {
                    charactersHit.Add(GameLogic.characters[obj.id]);
                }

            }
        }

        return charactersHit;
    }

    public static List<CharacterObject> findCharactersInRadius(int excludeCharacterId, int turnNumber, Vector2 point, float radius)
    {
        List<CharacterObject> charactersHit = new List<CharacterObject>();
        foreach (CharacterStateData obj in GameLogic.gameStates[turnNumber].characters)
        {
            Vector2 position = obj.position;
            float diffLength = (position - point).Length();
            if (diffLength <= radius + characterHitBoxRadius && obj.id != excludeCharacterId)
            {
                if(GameLogic.characters.ContainsKey(obj.id))
                {
                    charactersHit.Add(GameLogic.characters[obj.id]);
                }
                
            }
        }

        return charactersHit;
    }
}

