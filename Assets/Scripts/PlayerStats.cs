using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _energy;
    [SerializeField] private int _startEnergy;
    [SerializeField] private GamePlayUI _gamePlayUI;
    [SerializeField] private int _lives;
    [SerializeField] private int _startLives;
    [SerializeField] private GameManager _gameManager;

    void Start()
    {
        _energy = _startEnergy;
        _lives = _startLives;
        _gamePlayUI.UpdateEnergyText(_energy);
        _gamePlayUI.UpdateLivesText(_lives);
    }

    void Update()
    {
        if (_lives <= 0)
        {
            _gameManager.GameOver();
        }
    }

    public void SpendEnergy(int amount)
    {
        _energy -= amount;
        _gamePlayUI.UpdateEnergyText(_energy);
    }

    public void GainEnergy(int amount)
    {
        _energy += amount;
        _gamePlayUI.UpdateEnergyText(_energy);
    }

    public int GetEnergy()
    {
        return _energy;
    }

    public void ReduceLives()
    {
        if (_lives > 0)
        {
            _lives--;
        }
        else
        {
            _lives = 0;
        }
        _gamePlayUI.UpdateLivesText(_lives);
    }

    public int GetLives()
    {
        return _lives;
    }
}
