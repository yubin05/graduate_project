using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryBossRoom : MonoBehaviour
{
    GameObject player;
    GameObject boss;
    GameObject entryBossRoomDoor;

    void Awake()
    {
        player = GameObject.FindWithTag("Player");
        boss = GameObject.FindWithTag("Boss");
        entryBossRoomDoor = GameObject.FindWithTag("EntryBossRoomDoor");
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // player stop wait a minute
            player.GetComponent<PlayerController>().canInput = false;
            StartCoroutine(CanMovePlayer());

            // close door in boss room
            entryBossRoomDoor.SetActive(true);

            // boss entry
            boss.SetActive(true);
        }
    }

    IEnumerator CanMovePlayer()
    {
        yield return new WaitForSeconds(1f);

        // player can move
        player.GetComponent<PlayerController>().canInput = true;

        // prevent iteration
        Destroy(gameObject);
    }
}
