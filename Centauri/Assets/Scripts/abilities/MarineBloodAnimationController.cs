using UnityEngine;


public class MarineBloodAnimationController : MonoBehaviour
{
    public float lifeTime = 0.6f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {

    }
}

