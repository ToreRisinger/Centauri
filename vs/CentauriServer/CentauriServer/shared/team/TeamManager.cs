public class TeamManager
{

    public Team marineTeam = new Team(ETeam.MARINE, "Marines");
    public Team centauriTeam = new Team(ETeam.CENTAURI, "Centauri");
    public Team spectatorTeam = new Team(ETeam.SPECTATORS, "Spectators");

    public TeamManager()
    {

    }

    public Team getTeam(ETeam teamId)
    {
        switch(teamId)
        {
            case ETeam.MARINE: return marineTeam;
            case ETeam.CENTAURI: return centauriTeam;
            case ETeam.SPECTATORS: return spectatorTeam;
            default: return spectatorTeam;
        }
    }
}

