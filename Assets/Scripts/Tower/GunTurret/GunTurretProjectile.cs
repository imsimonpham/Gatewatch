using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GunTurretProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dmg;
    private GameObject _targetShadow;
    [SerializeField]  private float _dmgRadius;
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
        HitTarget();
    }

    void HitTarget()
    {
        Vector3 dir = _targetShadow.transform.position - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * _speed, Space.World);
        float distance = Vector3.Distance(_targetShadow.transform.position, transform.position);
        if (distance <= 0.5f)
        {
            DealDamage();
            InstantiateHitImpact();
            Destroy(gameObject);
        }
    }

    public void DealDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _dmgRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(_groundEnemyTag) || collider.CompareTag(_airEnemyTag))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(_dmg);
            }
        }
    }

    public void SeekTargetShadow(GameObject targetShadow)
    {
        _targetShadow = targetShadow;
    }
    
    void InstantiateHitImpact()
    {
        GameObject impact = Instantiate(_hitImpactPrefab, transform.position, Quaternion.identity);
        impact.transform.parent = _bulletContainer.transform;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255, 240, 142, 20);
        Gizmos.DrawSphere(transform.position, _dmgRadius);
    }
}
