using System.Collections.Generic;
using System.Numerics;

namespace Shared
{
    class PlayerMovement
    {
        private static float moveSpeed = 3f;

        public static Vector2 GetNewPosition(Vector2 _position, HashSet<EPlayerAction> _actions, float _delta)
        {
            Vector2 _inputDirection = Vector2.Zero;
            if (_actions.Contains(EPlayerAction.UP))
            {
                _inputDirection.Y += 1;
            }
            if (_actions.Contains(EPlayerAction.LEFT))
            {
                _inputDirection.X -= 1;
            }
            if (_actions.Contains(EPlayerAction.DOWN))
            {
                _inputDirection.Y -= 1;
            }
            if (_actions.Contains(EPlayerAction.RIGHT))
            {
                _inputDirection.X += 1;
            }

            Vector2 newPosition = new Vector2(_position.X, _position.Y);
            Vector2 direction = new Vector2(_inputDirection.X, _inputDirection.Y);
            if (direction.Length() > 0)
            {
                Vector2 _direction = Vector2.Normalize(direction);
                newPosition += _direction * moveSpeed * _delta;
            }

            return newPosition;
        }
    }
}
