using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CrackWall : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] public AudioClip destroyed_audio;

    private Tilemap tilemap;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = destroyed_audio;

        tilemap = gameObject.GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "ThrowObject")
        {
            tilemap.color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            audioSource.Play();
            StartCoroutine(Destroying());
        }
    }

    IEnumerator Destroying()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
