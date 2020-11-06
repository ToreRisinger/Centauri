using UnityEngine;

public class RoachBiteAnimationController : MonoBehaviour
{
    public float lifeTime = 0.6f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
