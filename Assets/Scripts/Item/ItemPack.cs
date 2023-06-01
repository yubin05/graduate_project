using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemPack : MonoBehaviour
{
    [SerializeField] public int addValue;

    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            GameObject player = collider.gameObject;
            PlayerController player_script = player.GetComponent<PlayerController>();

            // actually item implement
            TriggeredItem(player, player_script);

            Destroy(gameObject);
        }
    }

    protected abstract void TriggeredItem(GameObject player, PlayerController player_script);
}
