using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterObject : GGameObject
{

    public Sprite topSprite;
    public Sprite topRightSprite;
    public Sprite rightSprite;
    public Sprite bottomRightSprite;
    public Sprite bottomSprite;
    public Sprite bottomLeftSprite;
    public Sprite leftSprite;
    public Sprite topLeftSprite;

    public ECharacterType type;
    public EObjectDirection direction;
    public ETeam teamId;
    public int hp;
    public int speed;

    private Rigidbody2D rb;
    private bool isLocalCharacter = false;


    void Update()
    {
        SetSprite();
    }

    public void SetDirection(HashSet<EPlayerAction> _actions)
    {
        if (_actions.Contains(EPlayerAction.HOLD_DIRECTION))
        {
            return;
        }

        bool up = _actions.Contains(EPlayerAction.UP);
        bool left = _actions.Contains(EPlayerAction.LEFT);
        bool right = _actions.Contains(EPlayerAction.RIGHT);
        bool down = _actions.Contains(EPlayerAction.DOWN);

        if (up || down || left || right)
        {
            direction = PlayerDirectionUtils.GetPlayerDirection(up, down, left, right);
        }
    }

    public void SetAsLocalPlayer()
    {
        isLocalCharacter = true;
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.freezeRotation = true;
        rb.gravityScale = 0;

        CapsuleCollider2D collider = gameObject.AddComponent<CapsuleCollider2D>();
        collider.offset = new Vector2(0, (float)0.3);
        collider.size = new Vector2(2, 1);
    }

    public new void Move(Vector2 _newPosition)
    {
        if(isLocalCharacter)
        {
            rb.MovePosition(_newPosition);
        } else
        {
            base.Move(_newPosition);
        }
    }

    protected Sprite GetBottomLeftSprite()
    {
        return bottomLeftSprite;
    }

    protected Sprite GetBottomRightSprite()
    {
        return bottomRightSprite;
    }

    protected Sprite GetBottomSprite()
    {
        return bottomSprite;
    }

    protected Sprite GetLeftSprite()
    {
        return leftSprite;
    }

    protected Sprite GetRightSprite()
    {
        return rightSprite;
    }

    protected Sprite GetTopLeftSprite()
    {
        return topLeftSprite;
    }

    protected Sprite GetTopRightSprite()
    {
        return topRightSprite;
    }

    protected Sprite GetTopSprite()
    {
        return topSprite;
    }

    private void SetSprite()
    {
        switch (direction)
        {
            case EObjectDirection.TOP:
                spriteRenderer.sprite = GetTopSprite();
                return;
            case EObjectDirection.TOP_RIGHT:
                spriteRenderer.sprite = GetTopRightSprite();
                return;
            case EObjectDirection.RIGHT:
                spriteRenderer.sprite = GetRightSprite();
                return;
            case EObjectDirection.BOTTOM_RIGHT:
                spriteRenderer.sprite = GetBottomRightSprite();
                return;
            case EObjectDirection.BOTTOM:
                spriteRenderer.sprite = GetBottomSprite();
                return;
            case EObjectDirection.BOTTOM_LEFT:
                spriteRenderer.sprite = GetBottomLeftSprite();
                return;
            case EObjectDirection.LEFT:
                spriteRenderer.sprite = GetLeftSprite();
                return;
            case EObjectDirection.TOP_LEFT:
                spriteRenderer.sprite = GetTopLeftSprite();
                return;
            default:
                spriteRenderer.sprite = GetTopSprite();
                return;
        }
    }

}
