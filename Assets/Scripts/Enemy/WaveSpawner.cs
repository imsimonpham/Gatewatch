using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveData[] _waveData;
    [SerializeField] private GameObject _groundSpawnPoint;
    [SerializeField] private GameObject _airSpawnPoint;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private float _timeBetweenWaves = 5f;
    [SerializeField] private float _countdown = 3f;

    private int _waveIndex;
    private int _enemiesAlivePerWave;
    private int _enemyIndex;
    private string _groundEnemyTag = "GroundEnemy";
    private string _airEnemyTag = "AirEnemy";

    [SerializeField] private GamePlayUI _gamePlayUI;

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

        if (_waveIndex < _waveData.Length)
        {
            _countdown -= Time.deltaTime;
            _gamePlayUI.UpDateCountdownText(_countdown);
        }
    }

    IEnumerator SpawnWave()
    {
        WaveData waveData = _waveData[_waveIndex];
        _waveIndex++;
        foreach (EnemyData enemyData in waveData.GetEnemyDataList())
        {
            for (int i = 0; i < enemyData.GetEnemyCount(); i++)
            {
                SpawnEnemy(enemyData.GetEnemyPrefab());
                yield return new WaitForSeconds(1f / enemyData.GetEnemySpawnRate());
            }
        }
    }

    void SpawnEnemy(GameObject enemyPrefab)
    {
        if (enemyPrefab.CompareTag(_groundEnemyTag))
        {
            //Get the spawn rotation in the opposite direction of the spawn point
            Quaternion spawnRotation = Quaternion.Euler(0f, _groundSpawnPoint.transform.rotation.eulerAngles.y - 180f, 0f);
            
            //Get a random x and z position within the portal 
            float x = Random.Range(_groundSpawnPoint.transform.position.x - 3f, _groundSpawnPoint.transform.position.x + 3f);
            float z = Random.Range(_groundSpawnPoint.transform.position.z - 3f, _groundSpawnPoint.transform.position.z + 3f);
            Vector3 spawnPos = new Vector3(x, _groundSpawnPoint.transform.position.y, z);
            
            Enemy enemy = Instantiate(enemyPrefab,spawnPos, spawnRotation).GetComponent<Enemy>();
            enemy.transform.parent = _enemyContainer.transform;
        }
        else
        {
            //Get the spawn rotation in the opposite direction of the spawn point
            Quaternion spawnRotation = Quaternion.Euler(0f, _airSpawnPoint.transform.rotation.eulerAngles.y - 180f, 0f);
            
            //Get a random x and z position within the portal
            float x = Random.Range(_airSpawnPoint.transform.position.x - 3f, _airSpawnPoint.transform.position.x + 3f);
            float z = Random.Range(_airSpawnPoint.transform.position.z - 3f, _airSpawnPoint.transform.position.z + 3f);
            Vector3 spawnPos = new Vector3(x, _airSpawnPoint.transform.position.y, z);
            
            Enemy enemy = Instantiate(enemyPrefab, spawnPos, spawnRotation).GetComponent<Enemy>();
            enemy.transform.parent = _enemyContainer.transform;
        }
        _enemyIndex++;
        _enemiesAlivePerWave++;
    }

    public void ReduceEnemiesAlive()
    {
        _enemiesAlivePerWave--;
    }
}
