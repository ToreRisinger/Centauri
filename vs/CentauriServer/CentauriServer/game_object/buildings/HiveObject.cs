
using System.Numerics;

public class HiveObject : BuildingObject
{
    public HiveObject(Vector2 _position) : 
        base(EBuildingType.HIVE, Stats.HIVE_HP, Stats.HIVE_REGEN, TeamManager.centauriTeam.GetTeamId(), _position)
    {

    }
}

