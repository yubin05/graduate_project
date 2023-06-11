using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script insert in TriggerDestroyWall object
// TriggerDestroyWall object insert in enemy object
public class TriggerDestroyWall : MonoBehaviour
{
    [SerializeField] public GameObject target_wall;

    private void OnDestroy()
    {
        if (target_wall != null) { Destroy(target_wall); }
    }
}
