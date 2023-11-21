using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _groundSpawnPoint;
    private GameObject _endPoint;
    private GameObject _airSpawnPoint;
    [SerializeField] private NavMeshAgent _agent;
    private WaveSpawner _waveSpawner;
    private string _groundEnemyTag = "GroundEnemy";
    private string _airEnemyTag = "AirEnemy";
    private string _endPointTag = "EndPoint";
    private Vector3 dir;
    
    void Start()
    {
        //Cache Components
        _groundSpawnPoint = GameObject.Find("GroundSpawnPoint");
        if (_groundSpawnPoint == null)
        {
            Debug.LogError("Ground Spawn Point is null");
        }

        _endPoint = GameObject.Find("EndPoint2");
        if (_endPoint == null)
        {
            Debug.LogError("End Point 2 is null");
        }
        
        _airSpawnPoint = GameObject.Find("AirSpawnPoint");
        if (_airSpawnPoint == null)
        {
            Debug.LogError("Air Spawn Point is null");
        }
        
        _waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        if (_waveSpawner == null)
        {
            Debug.LogError("Wave Spawner is null");
        }
        
        //transform.position = _groundSpawnPoint.transform.position;
    }

    void Update()
    {
        MoveToEndPoint();
    }

    void MoveToEndPoint()
    {
        _agent.SetDestination(_endPoint.transform.position);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_endPointTag))
        {
            _waveSpawner.ReduceEnemiesAlive();
            Destroy(gameObject);
        }
    }
}




