using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TowerBlueprint _towerToBuild;

    public bool HaveATowerToBuild()
    {
        return _towerToBuild != null ? true : false;
    }

    public void SetTowerToBuild(TowerBlueprint tower)
    {
        _towerToBuild = tower;
    }

    public void ClearTowerToBuld()
    {
        _towerToBuild = null;
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return _towerToBuild;
    }
}
