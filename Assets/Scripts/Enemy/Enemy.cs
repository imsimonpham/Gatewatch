using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _enemyIndex;

    public void SetEnemyIndex(int index)
    {
        _enemyIndex = index;
    }
}
