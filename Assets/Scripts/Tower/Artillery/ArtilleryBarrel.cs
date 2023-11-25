using UnityEngine;

public class ArtilleryBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireRate = 10f;
    private float _canFire = 0f;
    [SerializeField] private Artillery _mainTower;
    private GameObject _target;
    private GameObject _bulletContainer;

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
        _target = _mainTower.GetTarget();
        if (_canFire <= 0f && _target != null)
        {
            ShootProjectiles();
            _canFire = 1f / _fireRate;
        }

        _canFire -= Time.deltaTime;
    }

    private void ShootProjectiles()
    {
        GameObject projectileGO = Instantiate(_projectilePrefab, transform.position, _target.transform.rotation);
        projectileGO.transform.parent = _bulletContainer.transform;
        ArtilleryProjectile projectile = projectileGO.GetComponent<ArtilleryProjectile>();

        if (projectile != null)
        {
            projectile.SeekTarget(_target);
        }
    }
}