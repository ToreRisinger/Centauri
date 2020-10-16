using System.Numerics;

public class Map
{
    public static Vector2 mapPosition = new Vector2(10000, 10000);

    private int mapId;
    private Vector2 marineSpawnPoint;
    private Vector2 alienSpawnPoint;

    public Map(int _mapId, Vector2 _marineSpawnPoint, Vector2 _alienSpawnPoint)
    {
        mapId = _mapId;
        marineSpawnPoint = _marineSpawnPoint;
        alienSpawnPoint = _alienSpawnPoint;
    }

    public Vector2 getMarineSpawnPoint()
    {
        return marineSpawnPoint;
    }

    public Vector2 getAlienSpawnPoint()
    {
        return alienSpawnPoint;
    }

    public int GetMapId()
    {
        return mapId;
    }
}

