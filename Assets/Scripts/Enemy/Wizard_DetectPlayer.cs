using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_DetectPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") 
        {
            //gameManager_script.SetDetectTeleportPointTrigger(transform.parent.name, true);

            transform.parent.Find("Detect_Teleport_Point").gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //gameManager_script.SetDetectTeleportPointTrigger(transform.parent.name, false);

            transform.parent.Find("Detect_Teleport_Point").gameObject.SetActive(false);
        }
    }
}
