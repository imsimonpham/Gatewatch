using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _gameOver;

    void Start()
    {
        _gameOver = false;
    }

    void Update()
    {
        if (_gameOver)
        {
            Debug.Log("Game Over!");
        }
    }

    public void GameOver()
    {
        _gameOver = true;
    }
}
