using UnityEngine;

public class AntiAirCrafterBarrel : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private float _fireRate = 10f;
    private float _canFire = 0f;
    [SerializeField] private AntiAircrafter _mainTower;
    private GameObject _target;

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
        AntiAircrafterProjectile projectile = projectileGO.GetComponent<AntiAircrafterProjectile>();

        if (projectile != null)
        {
            projectile.SeekTarget(_target);
        }
    }
}
