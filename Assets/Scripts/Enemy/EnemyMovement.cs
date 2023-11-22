using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    private WaveSpawner _waveSpawner;
    private string _endPointTag = "EndPoint";
    
    void Start()
    {
        _waveSpawner = GameObject.Find("WaveSpawner").GetComponent<WaveSpawner>();
        if (_waveSpawner == null)
        {
            Debug.LogError("Wave Spawner is null");
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
            Destroy(gameObject);
        }
    }
}




