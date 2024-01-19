using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _enemyIndex;
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private int _enemyEnergyWorth;
    [SerializeField] private GameObject _targetPoint;
    [SerializeField] private int _waveIndex;
    
    [SerializeField] private Image _healthBarImg;
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

    public void SetEnemyWaveIndex(int index)
    {
        _waveIndex = index;
    }

    public int GetEnemyWaveIndex()
    {
        return _waveIndex;
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
        Debug.Log("Wave Index Minus 1: " + (_waveSpawner.GetCurrentWaveIndex() - 1));
        if (_waveSpawner.GetCurrentWaveIndex() - 1 == _waveIndex)
        {
            _waveSpawner.CountEnemiesKilledPerWave();
        }
        Debug.Log("Total Enemies Spawned: " + _waveSpawner.GetTotalEnemiesPerWave());
        Debug.Log("Total Enemies Killed: " + _waveSpawner.GetEnemiesKilledPerWave());
        Destroy(gameObject);
    }
   

    void UpdateHealthBar()
    {
        _healthBarImg.fillAmount = _health/_maxHealth;
    }

    public GameObject GetTargetPoint()
    {
        return _targetPoint;
    }
}
