
using System.Collections.Generic;
using System.Numerics;

public class CharacterObject : HpObject
{
    private Queue<PlayerCommandData> commandQueue;
    private List<PlayerCommandData> commandHistory;

    public ECharacterType characterType;
    public EObjectDirection direction;

    public int playerId;

    public int speed;

    public CharacterObject(int _playerId, ECharacterType _characterType, int _hp, int _regen, int _speed, ETeam _teamId, Vector2 _position, EObjectDirection _direction) : 
        base(_hp, _regen, _teamId, _position)
    {
        playerId = _playerId;
        direction = _direction;
        characterType = _characterType;
        speed = _speed;

        commandQueue = new Queue<PlayerCommandData>();
        commandHistory = new List<PlayerCommandData>();
    }

    public void pushCommand(PlayerCommandData _command)
    {
        commandQueue.Enqueue(_command);
    }

    public new void Update()
    {
        base.Update();

        while (commandQueue.Count > 0)
        {
            //TODO add validation checks
            PlayerCommandData command = commandQueue.Dequeue();
            position = command.position;
            direction = command.direction;

            commandHistory.Add(command);
        }

        while (commandHistory.Count > 10)
        {
            commandHistory.RemoveAt(0);
        }
    }
}
