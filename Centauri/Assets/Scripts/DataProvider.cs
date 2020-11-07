using System.Collections.Generic;

public static class DataProvider
{
    public static Dictionary<int, CharacterObject> getCharacters()
    {
        return GameManager.characters;
    }
}

