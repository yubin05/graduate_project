using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowObjectController : MonoBehaviour
{
    public GameObject targetGameObject;

    // Audio Manager
    GameObject audio_;
    AudioManager audioManager;

    // this variable controled by other scripts
    public float moveSpeed;
    public float throw_delay;

    private void Start()
    {
        // get audio manager
        audio_ = GameObject.Find("AudioManager_Player");
        audioManager = audio_.GetComponent<AudioManager_Player>();
    }

    // this method called by PlayerController.cs
    public void InstantiateClone(bool flipX)
    {
        // Instantiate
        GameObject newObject = Instantiate(targetGameObject, transform.position, transform.rotation) as GameObject;
        if (flipX) { newObject.transform.localScale *= -1; }

        // sound play
        audioManager.PlayOneShotAudio("Throw");
    }
}
