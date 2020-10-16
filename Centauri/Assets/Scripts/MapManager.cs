
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;

    /*
     * All map prefabs
     * 
     */
    public GameObject testMapPrefab;

    private Dictionary<int, Map> maps;
    private Dictionary<int, GameObject> mapPrefabs;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }


        maps = new Dictionary<int, Map>();
        mapPrefabs = new Dictionary<int, GameObject>();

        TestMap testMap = new TestMap();
        maps.Add(testMap.GetMapId(), testMap);
        mapPrefabs.Add(testMap.GetMapId(), testMapPrefab);
    }

    public Map GetMap(int mapId)
    {
        return maps[mapId];
    }

    public GameObject GetMapPrefab(int mapId)
    {
        return mapPrefabs[mapId];
    }
}

