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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player_script.isGrounded = false;
            //GetComponent<CompositeCollider2D>().isTrigger = false;
        }
    }

    //private void OnTriggerEnter2D(Collider2D collider)
    //{      
    //    // detect floor
    //    if (collider.gameObject.tag == "Floor")
    //    {
    //        //Debug.Log(transform.position.y - collider.transform.position.y);  // parameter test
    //        if (transform.position.y - collider.transform.position.y > floorColiderSubtract)
    //        {
    //            collider.isTrigger = false;
    //            Landing();
    //        }
    //    }
    //}
}
