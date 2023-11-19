using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _spawnPoint;
    private GameObject _endPoint;
    [SerializeField] private NavMeshAgent _agent;
    private WaveSpawner _waveSpawner;
    
    void Start()
    {
        //Cache Components
        _spawnPoint = GameObject.Find("SpawnPoint");
        if (_spawnPoint == null)
        {
            Debug.LogError("Spawn Point is null");
        }
        _endPoint = GameObject.Find("EndPoint");
        if (_endPoint == null)
        {
            Debug.LogError("End Point is null");
        }

        _waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        if (_waveSpawner == null)
        {
            Debug.LogError("Wave Spawner is null");
        }
        
        transform.position = _spawnPoint.transform.position;
    }

    void Update()
    {
        MoveToEndPoint();
    }

    void MoveToEndPoint()
    {
        _agent.SetDestination(_endPoint.transform.position);

        Vector3 dir = _endPoint.transform.position - transform.position;
        if (dir.magnitude <= 0.1f)
        {
            Destroy(gameObject);
            _waveSpawner.ReduceEnemiesAlive();
        }
    }
}




