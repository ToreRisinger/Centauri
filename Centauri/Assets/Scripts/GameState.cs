using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class GameState
{
    public int turnNumber;
    public List<PlayerStateData> players;
    
    public GameState(int _turnNumber, List<PlayerStateData> _players)
    {
        turnNumber = _turnNumber;
        players = _players;
    }
}

