using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject startPoint;  Transform startPoint_transform;

    [SerializeField] public GameObject player_prefab;
    [SerializeField] public float respawn_cooltime;

    [HideInInspector] public bool isPlayerDead = false;

    private void Awake()
    {
        startPoint = GameObject.FindWithTag("Respawn");
        if (startPoint != null) { startPoint_transform = startPoint.transform; }
    }

    private void Start()
    {
        GameObject teleport_points = GameObject.Find("Teleport_Points");
        if (teleport_points)
        {
            Transform[] allChildren = teleport_points.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                // except parent object (== self)
                if (child.name == teleport_points.name) { continue; }

                SpriteRenderer child_sprite = child.GetComponent<SpriteRenderer>();
                child_sprite.color = new Color(1, 1, 1, 0);     // invisible
            }
        }
    }

    private void Update()
    {
        Respawn();
    }

    // this method called by PlayerController.cs
    public void Respawn()
    {
        if (!isPlayerDead) { return; }
        StartCoroutine(GameOverRetry());
        isPlayerDead = false;
    }

    IEnumerator GameOverRetry()
    {
        yield return new WaitForSeconds(respawn_cooltime);
        //Instantiate(player_prefab, startPoint_transform.position, startPoint_transform.rotation);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // self scene reload
    }
}
