using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    protected GameObject player;
    protected PlayerController player_script;

    /*protected readonly string parentClassName = "Platform";
    protected IEnumerator IEAfterLanding;
    protected WaitForSeconds waitForSeconds;*/

    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerController>();

        /*IEAfterLanding = AfterLanding();
        waitForSeconds = new WaitForSeconds(1f);*/
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // Player is Landing
        if (collision.gameObject.tag == "Player" && collision.collider == player.GetComponent<CapsuleCollider2D>())
        {
            //Debug.Log("Landing");
            player_script.Landing();
        }

        /*if (GetType().Name == parentClassName)
        {
            StartCoroutine(IEAfterLanding);
        }*/
            
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (!player_script.bInputJumpKey) return;

        if (collision.gameObject.tag == "Player" && collision.collider == player.GetComponent<CapsuleCollider2D>())
        {
            player_script.isGrounded = false;
        }
    }

    /*// platform, floor 겹침으로 일어나는 버그 방지 위한 코루틴
    protected IEnumerator AfterLanding()
    {
        yield return waitForSeconds;
        Debug.Log("AfterLanding");
        player_script.isGrounded = true;
    }*/
}
