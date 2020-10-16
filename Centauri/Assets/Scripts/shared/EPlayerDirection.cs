

namespace PlayerDirection
{
    public enum EPlayerDirection
    {
        TOP,
        TOP_RIGHT,
        RIGHT,
        BOTTOM_RIGHT,
        BOTTOM,
        BOTTOM_LEFT,
        LEFT,
        TOP_LEFT
    }

    public class PlayerDirectionUtils
    {
        public static EPlayerDirection GetPlayerDirection(bool up, bool down, bool left, bool right)
        {
            if(up && !left && !right && !down)
            {
                return EPlayerDirection.TOP;
            } 
            else if(up && !left && right && !down)
            {
                return EPlayerDirection.TOP_RIGHT;
            }
            else if (!up && !left && right && !down)
            {
                return EPlayerDirection.RIGHT;
            }
            else if (!up && !left && right && down)
            {
                return EPlayerDirection.BOTTOM_RIGHT;
            }
            else if (!up && !left && !right && down)
            {
                return EPlayerDirection.BOTTOM;
            }
            else if (!up && left && !right && down)
            {
                return EPlayerDirection.BOTTOM_LEFT;
            }
            else if (!up && left && !right && !down)
            {
                return EPlayerDirection.LEFT;
            }
            else if (up && left && !right && !down)
            {
                return EPlayerDirection.TOP_LEFT;
            }

            return EPlayerDirection.TOP;
        }
    }
}


