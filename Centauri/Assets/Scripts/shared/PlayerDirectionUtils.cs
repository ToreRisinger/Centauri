public class PlayerDirectionUtils
{
    public static EObjectDirection GetPlayerDirection(bool up, bool down, bool left, bool right)
    {
        if (up && !left && !right && !down)
        {
            return EObjectDirection.TOP;
        }
        else if (up && !left && right && !down)
        {
            return EObjectDirection.TOP_RIGHT;
        }
        else if (!up && !left && right && !down)
        {
            return EObjectDirection.RIGHT;
        }
        else if (!up && !left && right && down)
        {
            return EObjectDirection.BOTTOM_RIGHT;
        }
        else if (!up && !left && !right && down)
        {
            return EObjectDirection.BOTTOM;
        }
        else if (!up && left && !right && down)
        {
            return EObjectDirection.BOTTOM_LEFT;
        }
        else if (!up && left && !right && !down)
        {
            return EObjectDirection.LEFT;
        }
        else if (up && left && !right && !down)
        {
            return EObjectDirection.TOP_LEFT;
        }

        return EObjectDirection.TOP;
    }
}