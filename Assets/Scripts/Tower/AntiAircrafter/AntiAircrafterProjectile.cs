using System;
using UnityEngine;

public class AntiAircrafterProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    private GameObject _target;
    [SerializeField] private GameObject _hitImpactPrefab;
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = _target.transform.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * _speed, Space.World); 
    }

    public void SeekTarget(GameObject target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(_airEnemyTag) || other.gameObject.CompareTag(_groundEnemyTag))
        {
            //Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
