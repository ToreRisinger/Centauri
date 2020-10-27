using System.Numerics;

public class CommandCenterObject : BuildingObject
{
    public CommandCenterObject(Vector2 _position) : 
        base(EBuildingType.COMMAND_CENTER, Stats.COMMAND_CENTER_HP, Stats.COMMAND_CENTER_REGEN, TeamManager.marineTeam.GetTeamId(), _position)
    {

    }
}

