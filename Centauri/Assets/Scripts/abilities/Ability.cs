

public abstract class Ability
{
    public string name;
    private float cooldownTime;
    private float timeLeft;

    public Ability(string _name, float _cooldownTime)
    {
        cooldownTime = _cooldownTime;
        name = _name;
        timeLeft = 0;
    }

    public float GetCooldown()
    {
        return cooldownTime;
    }

    public float GetTimeLeft()
    {
        return timeLeft;
    }

    public void Update(float delta)
    {
        if(timeLeft > 0)
        {
            timeLeft -= delta;
        }

        if(timeLeft < 0)
        {
            timeLeft = 0;
        }
    }

    public bool run(CharacterObject obj)
    {
        if(timeLeft == 0 && isEnabled())
        {
            timeLeft = cooldownTime;
            runInternal(obj);
            return true;
        }

        return false;
    }

    public abstract bool isEnabled();

    protected abstract void runInternal(CharacterObject obj);
}

