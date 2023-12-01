using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private WaveData[] _waveData;
    [SerializeField] private GameObject _enemyContainer;
 
    [SerializeField] private int _waveIndex;
    private int _enemyIndex;
    private int _enemiesKilledPerWave;
    private string _groundEnemyTag = "GroundEnemy";
    private string _airEnemyTag = "AirEnemy";
    private GameObject spawnPoint;
    private GameObject endPoint;
    private bool _startWave = false;
    private int _totalEnemiesPerWave;
    
    [SerializeField] private GamePlayUI _gamePlayUI;
    [SerializeField] private Button _startWaveButton;
    [SerializeField] private TMP_Text _startWaveButtonText;
    
    //Paths
    [SerializeField] private GameObject _groundSpawnPoint_1;
    [SerializeField] private GameObject _groundSpawnPoint_2;
    [SerializeField] private GameObject _groundSpawnPoint_3;
    [SerializeField] private GameObject _airSpawnPoint_1;
    [SerializeField] private GameObject _airSpawnPoint_2;
    [SerializeField] private GameObject _airSpawnPoint_3;
    [SerializeField] private GameObject _endPoint_1;
    [SerializeField] private GameObject _endPoint_2;
    [SerializeField] private GameObject _endPoint_3;
    
    void Start()
    {
        _waveIndex = 0;
        _enemyIndex = 0;
        _enemiesKilledPerWave = 0;
        _totalEnemiesPerWave = 0;
    }

    void Update()
    {
        if (_startWave)
        {
            _startWave = false;
            _startWaveButton.gameObject.SetActive(false);
            StartCoroutine(SpawnWave());
        }

        if (_enemiesKilledPerWave >= _totalEnemiesPerWave * 0.5f)
        {
            _startWaveButton.gameObject.SetActive(true);
        }
    }

    IEnumerator SpawnWave()
    {
        _enemiesKilledPerWave = 0;
        _totalEnemiesPerWave = 0;
        if (_waveIndex == _waveData.Length - 1)
        {
            _startWaveButton.gameObject.SetActive(false);
        }
        WaveData waveData = _waveData[_waveIndex];
        _gamePlayUI.UpdateWaveText(_waveIndex + 1, _waveData.Length);
        foreach (EnemyData enemyData in waveData.GetEnemyDataList())
        {
            _totalEnemiesPerWave += enemyData.GetEnemyCount();
        }
        foreach (EnemyData enemyData in waveData.GetEnemyDataList())
        {
            
            for (int i = 0; i < enemyData.GetEnemyCount(); i++)
            {
                GameObject enemyPrefab = enemyData.GetEnemyPrefab();
                string enemyPath = enemyData.GetEnemyPath().ToString();
                float enemySpawnRate = enemyData.GetEnemySpawnRate();
                
                //safeguard against spawn rate = 0
                if (enemySpawnRate <= 0)
                {
                    enemySpawnRate = 1f;
                }
                else
                {
                    enemySpawnRate = enemyData.GetEnemySpawnRate();
                }
                
                if (enemyPrefab.CompareTag(_groundEnemyTag))
                {
                    if (enemyPath == "Path1" && _groundSpawnPoint_1 != null && _endPoint_1 != null)
                    {
                        spawnPoint = _groundSpawnPoint_1;
                        endPoint = _endPoint_1;
                    } else if (enemyPath == "Path2" && _groundSpawnPoint_2 != null && _endPoint_2 != null)
                    { 
                        spawnPoint = _groundSpawnPoint_2;
                        endPoint = _endPoint_2;
                    }
                    else if(enemyPath == "Path3" && _groundSpawnPoint_3 != null && _endPoint_3 != null)
                    {
                        spawnPoint = _groundSpawnPoint_3;
                        endPoint = _endPoint_3;
                    }
                }
                else
                {
                    if (enemyPath == "Path1" && _groundSpawnPoint_1 != null && _endPoint_1 != null)
                    {
                        spawnPoint = _airSpawnPoint_1;
                        endPoint = _endPoint_1;
                    } else if (enemyPath == "Path2" && _groundSpawnPoint_2 != null && _endPoint_2 != null)
                    {
                        spawnPoint = _airSpawnPoint_2;
                        endPoint = _endPoint_2;
                    }
                    else if(enemyPath == "Path3" && _groundSpawnPoint_3 != null && _endPoint_3 != null)
                    {
                        spawnPoint = _airSpawnPoint_3;
                        endPoint = _endPoint_3;
                    }
                }
                //spawn
                SpawnEnemy(enemyData.GetEnemyPrefab(), spawnPoint, endPoint);
                yield return new WaitForSeconds(enemySpawnRate);
            }
        }
        _waveIndex++;
    }

    void SpawnEnemy(GameObject enemyPrefab, GameObject spawnPoint, GameObject endPoint)
    {
        //Get the spawn rotation in the opposite direction of the spawn point
        Quaternion spawnRotation = Quaternion.Euler(0f, spawnPoint.transform.rotation.eulerAngles.y - 180f, 0f);
            
        //Get a random x and z position within the portal 
        float x = Random.Range(spawnPoint.transform.position.x - 3f, spawnPoint.transform.position.x + 3f);
        float z = Random.Range(spawnPoint.transform.position.z - 3f, spawnPoint.transform.position.z + 3f);
        Vector3 spawnPos = new Vector3(x, spawnPoint.transform.position.y, z);
            
        Enemy enemy = Instantiate(enemyPrefab,spawnPos, spawnRotation).GetComponent<Enemy>();
        enemy.SetEnemyIndex(_enemyIndex);
        enemy.transform.parent = _enemyContainer.transform;
        
        //enemy move to endpoint
        EnemyMovement enemyMovement = enemy.gameObject.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.MoveToEndPoint(endPoint);
        }
        _enemyIndex++;
    }

    public void CountEnemiesKilledPerWave()
    {
        _enemiesKilledPerWave++;
    }

    public void StartWave()
    {
        _startWave = true;
    }
}
