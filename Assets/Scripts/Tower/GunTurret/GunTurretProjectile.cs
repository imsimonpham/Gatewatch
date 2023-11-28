using UnityEngine;

public class GunTurretProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dmg;
    private GameObject _target;
    [SerializeField] private GameObject _hitImpactPrefab;
    private GameObject _bulletContainer;
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";

    void Start()
    {
        _bulletContainer = GameObject.FindWithTag("BulletContainer");
        if (_bulletContainer == null)
        {
            Debug.LogError("Bullet Container is null");
        }
    }

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
            InstantiateHitImpact();
            Destroy(gameObject);
        }
    }
    
    void InstantiateHitImpact()
    {
        GameObject impact = Instantiate(_hitImpactPrefab, transform.position, Quaternion.identity);
        impact.transform.parent = _bulletContainer.transform;
    }
}
