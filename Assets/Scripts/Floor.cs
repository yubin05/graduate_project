using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    GameObject player;
    PlayerController player_script;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player is Landing
        if (collision.gameObject.tag == "Player" && collision.collider == player.GetComponent<CapsuleCollider2D>())
        {
            player_script.Landing();
        }
    }
}
