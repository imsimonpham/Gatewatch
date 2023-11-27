using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    [SerializeField] private GameObject _prefabGhost;
    
    [SerializeField] private GameObject _prefabLv1;
    [SerializeField] private GameObject _prefabLv2;
    [SerializeField] private GameObject _prefabLv3;

    [SerializeField] private int _buildCost;
    private int _saleGain;
    private int _towerLevel = 1;
    
    [SerializeField] private int _upgradeCostToLv2;
    [SerializeField] private int _upgradeCostToLv3;
    
    //Prefabs
    public GameObject GetPrefabLv1()
    {
        return _prefabLv1;
    }
    public GameObject GetPrefabLv2()
    {
        return _prefabLv2;
    }
    public GameObject GetPrefabLv3()
    {
        return _prefabLv3;
    }

    public GameObject GetTowerghost()
    {
        return _prefabGhost;
    }
    
    //Cost
    public int GetBuildCost()
    {
        return _buildCost;
    }
    public int GetUpgradeCostToLv2()
    {
        return _upgradeCostToLv2;
    }
    
    public int GetUpgradeCostToLv3()
    {
        return _upgradeCostToLv3;
    }
    
    //Gains
    public int GetTowerSaleGain(int towerLevel)
    {
        if (towerLevel == 1)
        {
            _saleGain = Mathf.CeilToInt(_buildCost * 0.8f);
        } else if (towerLevel == 2)
        {
            _saleGain = Mathf.CeilToInt(_buildCost * 0.8f + _upgradeCostToLv2 * 0.5f);
        }
        else
        {
            _saleGain = Mathf.CeilToInt(_buildCost * 0.8f + _upgradeCostToLv2 * 0.5f + _upgradeCostToLv3 * 0.5f);
        }
        return _saleGain;
    }
    
    //Level
    /*public int GetTowerLevel()
    {
        return _towerLevel;
    }

    public void SetTowerLevel(int level)
    {
        _towerLevel = level;
    }*/
}
