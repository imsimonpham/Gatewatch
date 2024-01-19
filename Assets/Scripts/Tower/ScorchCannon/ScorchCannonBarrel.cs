using UnityEngine;

public class ScorchCannonBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireRate;
    [SerializeField] private GameObject _firePoint; 
    private float _canFire = 0f;
    [SerializeField] private ScorchCannon _mainTower;

    private GameObject _bulletContainer;
    [SerializeField] private float _delayTime;
    [SerializeField] private GameObject _muzzleFlashPrefab;

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
        Vector3 pos = _firePoint.transform.position;
        Quaternion rot = _targetShadow.transform.rotation;
        GameObject projectileGO = Instantiate(_projectilePrefab, pos, rot);
        GameObject muzzleFlashGO = Instantiate(_muzzleFlashPrefab, pos, rot);
        muzzleFlashGO.transform.parent = _bulletContainer.transform;
        projectileGO.transform.parent = _bulletContainer.transform;
        ScorchCannonProjectile projectile = projectileGO.GetComponent<ScorchCannonProjectile>();
        if (projectile != null)
        {
            projectile.SeekTargetShadow(_targetShadow);
            projectile.SetFirePoint(_firePoint);
        }
    }
}