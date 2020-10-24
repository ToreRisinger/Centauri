public class TeamManager
{

    public Team marineTeam = new Team(0, "Marines");
    public Team centauriTeam = new Team(1, "Centauri");
    public Team spectatorTeam = new Team(2, "Spectators");

    public TeamManager()
    {

    }

    public Team getTeam(int teamId)
    {
        if (teamId == 0)
        {
            return marineTeam;
        }
        else if (teamId == 1)
        {
            return centauriTeam;
        }
        else
        {
            return spectatorTeam;
        }
    }
}

