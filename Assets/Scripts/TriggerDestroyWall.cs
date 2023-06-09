using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script insert in TriggerDestroyWall object
// TriggerDestroyWall object insert in enemy object
public class TriggerDestroyWall : MonoBehaviour
{
    [SerializeField] public GameObject target_wall;
    GameObject root_wall;

    private void Awake()
    {
        root_wall = target_wall.transform.parent.gameObject;
    }

    private void OnDestroy()
    {
        // unity prevent remaining collider bug after object destroy
        root_wall.SetActive(false); root_wall.SetActive(true);

        Destroy(target_wall);
    }
}
