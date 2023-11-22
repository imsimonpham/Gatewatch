using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

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
    [SerializeField]  [Range(1f, 10f)] private float _enemySpawnRate;
    private NavMeshAgent _enemyAgent;
    
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

    public NavMeshAgent GetEnemyAgent()
    {
        return _enemyAgent = _enemyPrefab.GetComponent<NavMeshAgent>();
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


