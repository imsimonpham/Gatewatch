using UnityEngine;

public class Artillery : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _range = 30f;
    
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";

    void Update()
    {
        ScanForTargets();
    }

    private void ScanForTargets()
    {
        //Find all colliders in the tower's range
        Collider[] colliders = Physics.OverlapSphere(transform.position, _range);
        
        //Target enemies based on index
        int smallestEnemyIndex = int.MaxValue;
        GameObject prioritizedEnemy = null;
        
        //Detect the enemies within range
        foreach (Collider collider in colliders)
        {
            //Artillery cannot target air enemies
            if (collider.CompareTag(_groundEnemyTag) || collider.CompareTag(_airEnemyTag))
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                int enemyIndex = enemy.GetEnemyIndex();

                if (enemyIndex < smallestEnemyIndex)
                {
                    smallestEnemyIndex = enemyIndex;
                    prioritizedEnemy = enemy.gameObject;
                }
            }
        }

        if (prioritizedEnemy != null)
        {
            _target = prioritizedEnemy;
        }
        else
        {
            _target = null;
        }
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 240, 142, 20);
        Gizmos.DrawSphere(transform.position, _range);
    }

    public GameObject GetTarget()
    {
        return _target;
    }
}
