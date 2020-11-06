using System.Collections.Generic;
using System.Numerics;

namespace Shared
{
    class PlayerMovement
    {

        public static Vector2 GetNewPosition(Vector2 _position, List<EPlayerAction> _actions, float _delta, int _speed)
        {
            if(_actions.Contains(EPlayerAction.HOLD_POSITION))
            {
                return _position;
            }

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
            
            float modifiedMoveSpeed = _speed;
            if(_actions.Contains(EPlayerAction.HOLD_DIRECTION))
            {
                modifiedMoveSpeed = modifiedMoveSpeed / 2;
            }
            

            if (direction.Length() > 0)
            {
                Vector2 _direction = Vector2.Normalize(direction);
                newPosition += _direction * modifiedMoveSpeed * _delta;
            }

            return newPosition;
        }
    }
}
