using System.Numerics;
public class BuildingObject : HpObject
{

    public EBuildingType buildingType;

    public BuildingObject(EBuildingType _buildingType, int _hp, int _regen, ETeam _teamId, Vector2 _position) : base(_hp, _regen, _teamId, _position)
    {
        buildingType = _buildingType;
    }
}
