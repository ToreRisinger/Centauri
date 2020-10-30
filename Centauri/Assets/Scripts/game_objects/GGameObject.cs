using UnityEngine;

public class GGameObject : MonoBehaviour
{
    public int id;

    protected SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void Move(Vector2 _newPosition)
    {
        transform.position = _newPosition;
    }
}

