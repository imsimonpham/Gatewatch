using UnityEngine;

public class TowerBaseUI : MonoBehaviour
{
    [SerializeField] private PlayerStats _playersStats;
    private TowerBase _selectedTowerBase;
    
    
    public void UpgradeTower(TowerBlueprint towerBlueprint)
    {
        Debug.Log("Upgrade Tower");
    }

    public void SellTower(TowerBlueprint towerBlueprint)
    {
        Debug.Log("Sell Tower");
    }

    public void SetSelectedTowerBase(TowerBase towerBase)
    {
        _selectedTowerBase = towerBase;
        transform.position = _selectedTowerBase.GetTowerUIPosition();
    }
}
