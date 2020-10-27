public static class TeamManager
{

    public static Team marineTeam = new Team(ETeam.MARINE, "Marines");
    public static Team centauriTeam = new Team(ETeam.CENTAURI, "Centauri");
    public static Team spectatorTeam = new Team(ETeam.SPECTATORS, "Spectators");
    public static Team neutralTeam = new Team(ETeam.NEUTRALS, "Neutral");


    public static Team getTeam(ETeam teamId)
    {
        switch(teamId)
        {
            case ETeam.MARINE: return marineTeam;
            case ETeam.CENTAURI: return centauriTeam;
            case ETeam.SPECTATORS: return spectatorTeam;
            case ETeam.NEUTRALS: return neutralTeam;
            default: return spectatorTeam;
        }
    }
}

