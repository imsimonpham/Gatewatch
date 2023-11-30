using UnityEngine;
using System;

public class ArtilleryProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dmg;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private GameObject _hitImpactPrefab;
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";
    private float _animation;
    private GameObject _firePoint;
    private GameObject _target;
    private GameObject _bulletContainer;
    [SerializeField] private float _height;

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
            Destroy(gameObject, 0.5f);
            return;
        }
        FollowParabolicArc();
    }

    public void SeekTarget(GameObject target)
    {
        _target = target;
    }

    public void SetFirePoint(GameObject firePoint)
    {
        _firePoint = firePoint;
    }

    private void OnTriggerEnter(Collider other)
    {
        AOEDamage();
        InstantiateHitImpact();
        Destroy(gameObject);
    }

    void InstantiateHitImpact()
    {
        Vector3 pos = _target.transform.position;
        GameObject impact = Instantiate(_hitImpactPrefab, pos, Quaternion.identity);
        impact.transform.parent = _bulletContainer.transform;
    }

    public void FollowParabolicArc()
    {
        _animation += Time.deltaTime;
        transform.position = Parabola(_firePoint.transform.position, _target.transform.position, _height, _animation);
    }
    private Vector3 Parabola (Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    void AOEDamage()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(_groundEnemyTag) || collider.CompareTag(_airEnemyTag))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                enemy.TakeDamage(_dmg);
            }
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = new Color32(255, 240, 142, 20);
        Gizmos.DrawSphere(transform.position, _explosionRadius);
    }
}
