using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    public enum PathType
    {
        Path1, 
        Path2, 
        Path3
    }
    [SerializeField] private PathType _enemyPath;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _enemySpawnRate = 1f;
    
    public PathType GetEnemyPath()
    {
        return _enemyPath;
    }
    
    public GameObject GetEnemyPrefab()
    {
        return _enemyPrefab;
    }

    public int GetEnemyCount()
    {
        return _enemyCount;
    }

    public float GetEnemySpawnRate()
    {
        return _enemySpawnRate;
    }
}

[System.Serializable]
public class WaveData
{
    [SerializeField] private List<EnemyData> _enemyDataList;

    public List<EnemyData> GetEnemyDataList()
    {
        return _enemyDataList;
    }
}


