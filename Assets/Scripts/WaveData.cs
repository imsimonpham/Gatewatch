using UnityEngine;

[System.Serializable]

public class WaveData
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
