
using UnityEngine;

public class MarineObject : CharacterObject
{

    public Sprite topSprite;
    public Sprite topRightSprite;
    public Sprite rightSprite;
    public Sprite bottomRightSprite;
    public Sprite bottomSprite;
    public Sprite bottomLeftSprite;
    public Sprite leftSprite;
    public Sprite topLeftSprite;

    protected override Sprite GetBottomLeftSprite()
    {
        return bottomLeftSprite;
    }

    protected override Sprite GetBottomRightSprite()
    {
        return bottomRightSprite;
    }

    protected override Sprite GetBottomSprite()
    {
        return bottomSprite;
    }

    protected override Sprite GetLeftSprite()
    {
        return leftSprite;
    }

    protected override Sprite GetRightSprite()
    {
        return rightSprite;
    }

    protected override Sprite GetTopLeftSprite()
    {
        return topLeftSprite;
    }

    protected override Sprite GetTopRightSprite()
    {
        return topRightSprite;
    }

    protected override Sprite GetTopSprite()
    {
        return topSprite;
    }
}

