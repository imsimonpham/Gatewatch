using UnityEngine;

public class ScorchCannon : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _range = 10f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private GameObject _horizontalRotator;
    //[SerializeField] private GameObject _verticalRotator;
    //[SerializeField] private GameObject _targetPointer;
    
    private string _airEnemyTag = "AirEnemy";
    private string _groundEnemyTag = "GroundEnemy";

    void Update()
    {
        ScanForTargets();
        if(_target == null)
        {
            return;
        }
        RotateToTarget();
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

        if (prioritizedEnemy != null)
        {
            _target = prioritizedEnemy;
        }
        else
        {
            _target = null;
        }
    }

    private void RotateToTarget()
    {
        Vector3 dir = _target.transform.position - transform.position;
        Quaternion lookToRotation = Quaternion.LookRotation(dir);
        Vector3 horizontalRotation = Quaternion.Lerp(_horizontalRotator.transform.rotation, lookToRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        //Vector3 verticalRotation = Quaternion.Lerp(_verticalRotator.transform.rotation, lookToRotation, _rotationSpeed * Time.deltaTime).eulerAngles;
        _horizontalRotator.transform.rotation = Quaternion.Euler(0f, horizontalRotation.y, 0f);
        //_verticalRotator.transform.rotation = Quaternion.Euler(verticalRotation.x, verticalRotation.y, 0f);
    }
    
    
    /*void OnDrawGizmos()
    {
        if(_target != null) { 
            Gizmos.color = Color.red;      
            Gizmos.DrawLine(_targetPointer.transform.position, _target.transform.position);
        }
    }*/
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color32(255, 240, 142, 100);
        Gizmos.DrawSphere(transform.position, _range);
    }

    public GameObject GetTarget()
    {
        return _target;
    }
}
