using UnityEngine;
using System.Collections;

public class GunTurretBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject[] _firePoints; 
    private float _canFire = 0f;
    [SerializeField] private GunTurret _mainTower;
    private GameObject _target;
    private GameObject _bulletContainer;
    [SerializeField] private float _delayTime;
    [SerializeField] private GameObject _muzzleFlashPrefab;

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
            StartCoroutine(ShootProjectilesWithDelay(_delayTime));
            _canFire = 1f / _fireRate;
        }

        _canFire -= Time.deltaTime;
    }

    private IEnumerator ShootProjectilesWithDelay(float delayTime)
    {
        foreach (GameObject firePoint in _firePoints)
        {
            GameObject projectileGO = Instantiate(_projectilePrefab, firePoint.transform.position, _target.transform.rotation);
            GameObject muzzleFlashGO = Instantiate(_muzzleFlashPrefab, firePoint.transform.position, (_target.transform.rotation * Quaternion.Euler(0, -90, 0)));
            projectileGO.transform.parent = _bulletContainer.transform;
            muzzleFlashGO.transform.parent = _bulletContainer.transform;
            GunTurretProjectile projectile = projectileGO.GetComponent<GunTurretProjectile>();
            if (projectile != null)
            {
                projectile.SeekTarget(_target);
            }
            yield return new WaitForSeconds(delayTime);
        }
    }
}
