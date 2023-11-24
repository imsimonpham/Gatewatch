using UnityEngine;

[System.Serializable]
public class TowerBlueprint
{
    [SerializeField] private GameObject _prefabGhost;
    
    [SerializeField] private GameObject _prefabLv1;
    [SerializeField] private GameObject _prefabLv2;
    [SerializeField] private GameObject _prefabLv3;

    [SerializeField] private int _buildCost;
    
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
    
    //Player gains half of the build cost + half of the upgrade cost to that level. 
    
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
    
}
