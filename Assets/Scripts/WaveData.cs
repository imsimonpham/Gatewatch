using UnityEngine;
using System;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private int _enemyCount;
    [SerializeField] private float _spawnRate;

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