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
    private GameObject _target;
    private GameObject _bulletContainer;
    private Vector3 _lastPos;
    private Vector3 _pos;
    [SerializeField] private float _height;
    [SerializeField] private float _maxDistance;
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
        if (_target != null)
        {
            _pos = _target.transform.position;
            _lastPos = _pos;
            FollowParabolicArc(_lastPos);
        }
        else
        {
            _pos = _lastPos;
            FollowParabolicArc(_pos);
        }
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
        if (!other.gameObject.CompareTag(_groundEnemyTag))
        {
            Destroy(gameObject);
        }
        else
        {
            AOEDamage();
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                GameObject groundPoint = enemy.GetGroundPoint();
                Vector3 pos = new Vector3(groundPoint.transform.position.x, groundPoint.transform.position.y + 0.2f, groundPoint.transform.position.z);
                Quaternion rot = groundPoint.transform.rotation;
                InstantiateHitImpact(pos,rot);
            }
            Destroy(gameObject);
        }
    }
    

    void InstantiateHitImpact(Vector3 pos, Quaternion rot)
    {
        GameObject impact = Instantiate(_hitImpactPrefab, pos,rot);
        impact.transform.parent = _bulletContainer.transform;
    }

    public void FollowParabolicArc(Vector3 pos)
    {
        _animation += Time.deltaTime * _speed;
        transform.position = Parabola(_firePoint.transform.position, pos, _height, _animation);
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
