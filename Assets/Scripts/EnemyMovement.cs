using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private GameObject _spawnPoint;
    private GameObject _endPoint;
    [SerializeField] private NavMeshAgent _agent;
    
    void Start()
    {
        _spawnPoint = GameObject.Find("SpawnPoint");
        if (_spawnPoint == null)
        {
            Debug.Log("Spawn Point is null");
        }
        _endPoint = GameObject.Find("EndPoint");
        if (_endPoint == null)
        {
            Debug.Log("End Point is null");
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
    }
}




