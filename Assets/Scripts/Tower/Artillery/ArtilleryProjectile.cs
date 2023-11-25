using UnityEngine;

public class ArtilleryProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 50f;
    [SerializeField] private float _dmg = 2f;
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
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(_dmg);
            Destroy(gameObject);
        }
    }
}
