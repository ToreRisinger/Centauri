using System;
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

    public Vector2 topAttackPoint;
    public Vector2 topRightAttackPoint;
    public Vector2 rightAttackPoint;
    public Vector2 bottomRightAttackPoint;
    public Vector2 bottomAttackPoint;
    public Vector2 bottomLeftAttackPoint;
    public Vector2 leftAttackPoint;
    public Vector2 topLeftAttackPoint;

    public Vector2 attackPoint;
    public Vector2 attackOrigin;
    public Vector2 effectPoint;

    public ECharacterType type;
    public EObjectDirection direction;
    public ETeam teamId;
    public int hp;
    public int speed;

    private Rigidbody2D rb;
    private bool isLocalCharacter = false;

    void FixedUpdate()
    {
        float delta = Time.deltaTime;
        foreach(Ability ability in GetAbilities())
        {
            ability.Update(delta);
        }

    }

    void Update()
    {
        UpdateDirection();
    }

    public void SetDirection(List<EPlayerAction> _actions)
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

    public bool ActivateAbility(int index)
    {
        List<Ability> abilities = GetAbilities();
        if(index >= abilities.Count)
        {
            return false;
        }

        return abilities[index].run(this);
    }

    public Vector2 GetAttackOrigin()
    {
        return attackOrigin;
    }
    public Vector2 GetAttackPoint()
    {
        return attackPoint;
    }

    public Vector2 GetAttackDirection()
    {
        Vector2 attackOriginToAttackPoint = attackPoint - attackOrigin;
        return attackOriginToAttackPoint.normalized;
    }

    public bool IsLocalCharacter()
    {
        return isLocalCharacter;
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

    private void UpdateDirection()
    {
        switch (direction)
        {
            case EObjectDirection.TOP:
                spriteRenderer.sprite = GetTopSprite();
                attackPoint = topAttackPoint;
                return;
            case EObjectDirection.TOP_RIGHT:
                spriteRenderer.sprite = GetTopRightSprite();
                attackPoint = topRightAttackPoint;
                return;
            case EObjectDirection.RIGHT:
                spriteRenderer.sprite = GetRightSprite();
                attackPoint = rightAttackPoint;
                return;
            case EObjectDirection.BOTTOM_RIGHT:
                spriteRenderer.sprite = GetBottomRightSprite();
                attackPoint = bottomRightAttackPoint;
                return;
            case EObjectDirection.BOTTOM:
                spriteRenderer.sprite = GetBottomSprite();
                attackPoint = bottomAttackPoint;
                return;
            case EObjectDirection.BOTTOM_LEFT:
                spriteRenderer.sprite = GetBottomLeftSprite();
                attackPoint = bottomLeftAttackPoint;
                return;
            case EObjectDirection.LEFT:
                spriteRenderer.sprite = GetLeftSprite();
                attackPoint = leftAttackPoint;
                return;
            case EObjectDirection.TOP_LEFT:
                spriteRenderer.sprite = GetTopLeftSprite();
                attackPoint = topLeftAttackPoint;
                return;
            default:
                spriteRenderer.sprite = GetTopSprite();
                attackPoint = topLeftAttackPoint;
                return;
        }
    }

    public abstract List<Ability> GetAbilities();

    public abstract void takeDamage();
}
