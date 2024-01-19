using UnityEngine;
using System;

public class ScorchCannonProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _dmg;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private GameObject _hitImpactPrefab;
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";
    private string _groundTag = "Ground";
    private float _animation;
    private GameObject _firePoint;
    //private GameObject _target;
    private GameObject _bulletContainer;
    private Vector3 _lastPos;
    private Vector3 _pos;
    [SerializeField] private float _height;
    [SerializeField] private float _maxDistance;
    private GameObject _targetShadow;
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
        TriggerExplosion();
    }

    public void SeekTargetShadow(GameObject targetShadow)
    {
        _targetShadow = targetShadow;
    }

    public void SetFirePoint(GameObject firePoint)
    {
        _firePoint = firePoint;
    }

    void TriggerExplosion()
    {
        FollowParabolicArc(_targetShadow.transform.position);
        float distance = Vector3.Distance(_targetShadow.transform.position, transform.position);
        if (distance <= 1f)
        {
            AOEDamage();
            InstantiateHitImpact();
            Destroy(gameObject);
        }
    }

 
    void InstantiateHitImpact()
    {
        Vector3 pos = _targetShadow.transform.position + new Vector3(0f, 0.2f, 0f);
        GameObject impact = Instantiate(_hitImpactPrefab, pos, Quaternion.identity);
        impact.transform.parent = _bulletContainer.transform;
        Destroy(impact, 2f);
    }

    public void FollowParabolicArc(Vector3 pos)
    {
        _animation += Time.deltaTime * _speed;
        if (_firePoint != null)
        {
            transform.position = Parabola(_firePoint.transform.position, pos, _height, _animation);
        }
        else
        {
            Destroy(gameObject);
        }
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
            if (collider.CompareTag(_groundEnemyTag))
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
