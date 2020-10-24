using System.Collections.Generic;

public class Team
{
    private HashSet<int> playerIds;
    private ETeam teamId;
    private string teamName;

    public Team(ETeam _teamId, string _teamName)
    {
        teamId = _teamId;
        teamName = _teamName;
        playerIds = new HashSet<int>();
    }

    public void AddPlayer(int _id)
    {
        playerIds.Add(_id);
    }

    public void clearTeam()
    {
        playerIds.Clear();
    }

    public void RemovePlayer(int _id)
    {
        playerIds.Remove(_id);
    }

    public int GetNrOfPlayers()
    {
        return playerIds.Count;
    }

    public bool PlayerIsInTeam(int id)
    {
        return playerIds.Contains(id);
    }

    public string getTeamName()
    {
        return teamName;
    }

    public List<int> GetIds()
    {
        List<int> idList = new List<int>();
        foreach (int id in playerIds)
        {
            idList.Add(id);
        }

        return idList;
    }
    public ETeam GetTeamId()
    {
        return teamId;
    }
}

