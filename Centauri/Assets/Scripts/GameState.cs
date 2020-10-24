using System.Collections.Generic;

public class GameState
{
    public int turnNumber;
    public List<PlayerStateData> players;
    public Queue<Event> events;
    public GameState(int _turnNumber, List<PlayerStateData> _players, Queue<Event> _events)
    {
        turnNumber = _turnNumber;
        players = _players;
        events = _events;
    }
}

