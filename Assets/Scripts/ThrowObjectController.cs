using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectController : MonoBehaviour
{
    public GameObject targetGameObject;
    public float moveSpeed;
    public float throw_delay;

    public void InstantiateClone()
    {
        // Instantiate
        Instantiate(targetGameObject, transform.position, transform.rotation);
    }
}
