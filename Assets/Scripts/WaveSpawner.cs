using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveData[] _waveData;
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _countdown = 3f;

    private int _waveIndex;
    private int _maxWave = 2;
    private int _enemiesAlivePerWave;
    private int _enemyIndex;


    void Start()
    {
        _enemyIndex = 0;
        _waveIndex = 0;
    }

    void Update()
    {
        if (_countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            _countdown = _timeBetweenWaves;
            return;
        }

        if (_waveIndex < _maxWave)
        {
            _countdown -= Time.deltaTime;
        }
    }

    IEnumerator SpawnWave()
    {
        WaveData waveData = _waveData[_waveIndex];
        _waveIndex++;
        for (int i = 0; i < waveData.GetEnemyCount(); i++)
        {
            SpawnEnemy(waveData.GetEnemyPrefab());
            yield return new WaitForSeconds(1f / waveData.GetEnemySpawnRate());
        }

        //yield return null;
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        Enemy enemy = Instantiate(enemyPrefab, _spawnPoint.transform.position, Quaternion.identity).GetComponent<Enemy>();
        enemy.transform.parent = _enemyContainer.transform;
        _enemyIndex++;
        _enemiesAlivePerWave++;
    }

    public void ReduceEnemiesAlive()
    {
        _enemiesAlivePerWave--;
    }
}
