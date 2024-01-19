using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private GameObject _targetShadow;

    public void SetTargetShadow(GameObject targetShadow)
    {
        _targetShadow = targetShadow;
    }

    public GameObject GetTargetShadow()
    {
        return _targetShadow;
    }
}
