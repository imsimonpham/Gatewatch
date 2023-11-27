using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private WaveSpawner _waveSpawner;
    private string _endPointTag = "EndPoint";
    private GamePlayUI _gamePlayUI;
    private PlayerStats _playerStats;
    private GameManager _gameManager;
    
    void Start()
    {
        _waveSpawner = GameObject.FindWithTag("WaveSpawner").GetComponent<WaveSpawner>();
        if (_waveSpawner == null)
        {
            Debug.LogError("Wave Spawner is null");
        }
        
        _gamePlayUI = GameObject.FindWithTag("GamePlayUI").GetComponent<GamePlayUI>();
        if (_gamePlayUI == null)
        {
            Debug.LogError("Gameplay UI is null");
        }
        
        _playerStats = GameObject.FindWithTag("PlayerStats").GetComponent<PlayerStats>();
        if (_playerStats == null)
        {
            Debug.LogError("PlayerStats is null");
        }
        
        _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is null");
        }
    }
    

    public void MoveToEndPoint(GameObject endPoint)
    {
        _agent.SetDestination(endPoint.transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_endPointTag))
        {
            _waveSpawner.ReduceEnemiesAlive();
            _playerStats.ReduceLives();
            if (_playerStats.GetLives() <= 0)
            {
                _gameManager.GameOver();
            }
            Destroy(gameObject);
        }
    }
}




