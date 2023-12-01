using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _enemyIndex;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private int _enemyEnergyWorth;
    [SerializeField] private GameObject _groundPoint;
    
    [SerializeField] private Image _healthBarImg;
    private float _target;
    private float _reduceSpeed = 1f;
    private PlayerStats _playerStats;
    private WaveSpawner _waveSpawner;
    

    void Start()
    {
        _health = _maxHealth;
        _healthBarImg.fillAmount = _health;
        
        _playerStats = GameObject.FindWithTag("PlayerStats").GetComponent<PlayerStats>();
        if (_playerStats == null)
        {
            Debug.LogError("PlayerStats is null");
        }
        
        _waveSpawner = GameObject.FindWithTag("WaveSpawner").GetComponent<WaveSpawner>();
        if (_waveSpawner == null)
        {
            Debug.LogError("Wave Spawner is null");
        }
    }
    

    public void SetEnemyIndex(int index)
    {
        _enemyIndex = index;
    }
    public int GetEnemyIndex()
    {
        return _enemyIndex;
    }

    public void TakeDamage(float dmg)
    {
        _health -= dmg;
        UpdateHealthBar();
        if (_health <= 0)
        {
            _playerStats.GainEnergy(_enemyEnergyWorth);
            Die();
        }
    }
    

    public void Die()
    {
        _waveSpawner.CountEnemiesKilledPerWave();
        Destroy(gameObject);
    }
    

    void UpdateHealthBar()
    {
        _healthBarImg.fillAmount = _health/_maxHealth;
    }

    public GameObject GetGroundPoint()
    {
        return _groundPoint;
    }
}
