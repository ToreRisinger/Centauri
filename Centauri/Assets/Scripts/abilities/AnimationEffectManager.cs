using UnityEngine;

public class AnimationEffectManager : MonoBehaviour
{
    public static AnimationEffectManager instance;

    public GameObject roachBiteAnimationPrefab;
    public GameObject marineBloodAnimationPrefab;

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
    }

    public void MarineBlood(Vector2 position)
    {
        spawnAnimationObject(marineBloodAnimationPrefab, position);
    }

    public void RoachBite(Vector2 position)
    {
        spawnAnimationObject(roachBiteAnimationPrefab, position);
    }

    private void spawnAnimationObject(GameObject prefab, Vector2 position)
    {
        Instantiate(prefab, position, UnityEngine.Quaternion.Euler(UnityEngine.Vector3.forward));
    }

}
