using UnityEngine;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour
{
    public int id;
    public string username;
    public ETeam teamId; 

    public EObjectDirection direction;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public Sprite topSprite;
    public Sprite topRightSprite;
    public Sprite rightSprite;
    public Sprite bottomRightSprite;
    public Sprite bottomSprite;
    public Sprite bottomLeftSprite;
    public Sprite leftSprite;
    public Sprite topLeftSprite;

    void Awake()
    {
        direction = EObjectDirection.TOP;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        SetSprite();
    }

    public void Move(Vector2 _newPosition)
    {
        transform.position = _newPosition;
    }

    public void MoveVelocity(Vector2 _newPosition)
    {
        rb.MovePosition(_newPosition);
    }

    public void SetDirection(HashSet<EPlayerAction> _actions)
    {
        if(_actions.Contains(EPlayerAction.HOLD_DIRECTION))
        {
            return;
        }

        bool up = _actions.Contains(EPlayerAction.UP);
        bool left = _actions.Contains(EPlayerAction.LEFT);
        bool right = _actions.Contains(EPlayerAction.RIGHT);
        bool down = _actions.Contains(EPlayerAction.DOWN);

        if(up || down || left || right)
        {
            direction = PlayerDirectionUtils.GetPlayerDirection(up, down, left, right);
        } 
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void SetSprite()
    {
        switch(direction)
        {
            case EObjectDirection.TOP:
                spriteRenderer.sprite = topSprite;
                return;
            case EObjectDirection.TOP_RIGHT:
                spriteRenderer.sprite = topRightSprite;
                return;
            case EObjectDirection.RIGHT:
                spriteRenderer.sprite = rightSprite;
                return;
            case EObjectDirection.BOTTOM_RIGHT:
                spriteRenderer.sprite = bottomRightSprite;
                return;
            case EObjectDirection.BOTTOM:
                spriteRenderer.sprite = bottomSprite;
                return;
            case EObjectDirection.BOTTOM_LEFT:
                spriteRenderer.sprite = bottomLeftSprite;
                return;
            case EObjectDirection.LEFT:
                spriteRenderer.sprite = leftSprite;
                return;
            case EObjectDirection.TOP_LEFT:
                spriteRenderer.sprite = topLeftSprite;
                return;
            default:
                spriteRenderer.sprite = topSprite;
                return;
        }
    }
}
