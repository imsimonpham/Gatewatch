using UnityEngine;

public class Artillery : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _range = 30f;

    
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";

    private GameObject _targetShadow;
    private Tower _towerScript;
    private bool _haveATarget;

    private void Start()
    {
        InvokeRepeating("ScanForTargets", 0f, 0.1f);
        _towerScript = GetComponent<Tower>();
        if (_towerScript == null)
        {
            Debug.LogError("Tower Script is  null");
        }
        _targetShadow = _towerScript.GetTargetShadow();
        if (_targetShadow == null)
        {
            Debug.LogError("Target Shadow is  null");
        }
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

        //set target to be the prioritized enemy and set target shadow to follow it
        if (prioritizedEnemy != null)
        {
            _target = prioritizedEnemy;
            _targetShadow.transform.position = _target.GetComponent<Enemy>().GetGroundPoint().transform.position;
            _haveATarget = true;
        }
        else
        {
            _target = null;
            _haveATarget = false;
        }
    }

    public GameObject GetTargetShadowFromTowerBase()
    {
        return _targetShadow;
    }

    public bool HaveATarget()
    {
        return _haveATarget;
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 240, 142, 20);
        Gizmos.DrawSphere(transform.position, _range);
    }
}
