using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TowerBlueprint _towerToBuild;

    public bool HaveATowerToBuild()
    {
        return _towerToBuild != null;
    }

    public void SetTowerToBuild(TowerBlueprint tower)
    {
        _towerToBuild = tower;
    }

    public void ClearTowerToBuild()
    {
        _towerToBuild = null;
    }

    public TowerBlueprint GetTowerToBuild()
    {
        return _towerToBuild;
    }
}
