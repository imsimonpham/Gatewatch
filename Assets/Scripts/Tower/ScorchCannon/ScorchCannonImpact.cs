using UnityEngine;

public class ScorchCannonImpact : MonoBehaviour
{
    [SerializeField] private float _activeTime;
    [SerializeField] private GameObject _burnEffect;
    private string _groundEnemyTag = "GroundEnemy";
    [SerializeField] private float _dps;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestroy", _activeTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(_groundEnemyTag))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_dps * Time.deltaTime);
                _burnEffect.SetActive(true);
            }
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        _burnEffect.SetActive(false);
    }

    void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
