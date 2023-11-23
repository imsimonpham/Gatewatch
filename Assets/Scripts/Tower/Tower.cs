using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _range = 15f;


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
