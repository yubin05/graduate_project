using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryBossRoom : MonoBehaviour
{
    GameObject player;
    GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("success");

            // player stop wait a minute
            player.GetComponent<PlayerController>().canInput = false;

            // close door in boss room
            //code

            // boss entry
            boss.SetActive(true);

            // player can move
            player.GetComponent<PlayerController>().canInput = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("success");

            // player stop wait a minute
            player.GetComponent<PlayerController>().canInput = false;

            // close door in boss room
            //code

            // boss entry
            boss.SetActive(true);

            // player can move
            player.GetComponent<PlayerController>().canInput = true;
        }
    }
}
