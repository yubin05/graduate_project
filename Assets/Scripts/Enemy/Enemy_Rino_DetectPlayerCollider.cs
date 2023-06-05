using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Rino_DetectPlayerCollider : MonoBehaviour
{
    Rino rino_script;
    float plus_scaleX; float minus_scaleX;

    private void Start()
    {
        rino_script = transform.parent.GetComponent<Rino>();
        plus_scaleX = transform.localScale.x; minus_scaleX = plus_scaleX * (-1);
    }

    private void Update()
    {
        if (rino_script.render.flipX)
        {
            transform.localScale = new Vector3(
                minus_scaleX, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(
                plus_scaleX, transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            rino_script.Run();
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            rino_script.NotRun();
        }
    }
}
