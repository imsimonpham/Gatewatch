using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    public enum Path
    {
        Path1, 
        Path2, 
        Path3
    }
    [SerializeField] private Path _path;
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _spawnRate;
    
    public Path GetEnemyPath()
    {
        return _path;
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
        return _spawnRate;
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


