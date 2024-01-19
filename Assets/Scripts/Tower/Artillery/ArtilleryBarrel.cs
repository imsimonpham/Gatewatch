using UnityEngine;
using System.Collections;

public class ArtilleryBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _firePoint; 
    private float _canFire = 0f;
    [SerializeField] private Artillery _mainTower;
    private GameObject _targetShadow;
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
        if (_mainTower.HaveATarget())
        {
            _targetShadow = _mainTower.GetTargetShadowFromTowerBase();
            if (_canFire <= 0f)
            {
                Invoke("ShootProjectiles", _delayTime);
                _canFire = 1f / _fireRate;
            }
            _canFire -= Time.deltaTime;
        } 
    }

    private void ShootProjectiles()
    {
        GameObject projectileGO = Instantiate(_projectilePrefab, _firePoint.transform.position, _targetShadow.transform.rotation);
        GameObject muzzleFlashGO = Instantiate(_muzzleFlashPrefab, _firePoint.transform.position, _targetShadow.transform.rotation);
        muzzleFlashGO.transform.parent = _bulletContainer.transform;
        projectileGO.transform.parent = _bulletContainer.transform;
        ArtilleryProjectile projectile = projectileGO.GetComponent<ArtilleryProjectile>();
        if (projectile != null)
        {
            projectile.SeekTargetShadow(_targetShadow);
            projectile.SetFirePoint(_firePoint);
        }
    }
}