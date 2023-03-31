using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxControllor_Bandit : MonoBehaviour
{
    // hit box controller
    BoxCollider2D colliderOfCollision;

    // Boss_Bandit
    GameObject bandit;
    SpriteRenderer bandit_render;
    Boss_Bandit bandit_script;

    // player
    PlayerController player_script;

    private float offset_x_minus;
    private float offset_x_plus;

    private void Start()
    {
        colliderOfCollision = gameObject.GetComponent<BoxCollider2D>();

        bandit = transform.root.gameObject;
        bandit_render = bandit.GetComponent<SpriteRenderer>();
        bandit_script = bandit.GetComponent<Boss_Bandit>();
        offset_x_minus = colliderOfCollision.offset.x;
        offset_x_plus = offset_x_minus * (-1);
    }

    private void Update()
    {
        if (bandit_render.flipX)
        {
            colliderOfCollision.offset = new Vector2(offset_x_plus, colliderOfCollision.offset.y);
        }   // right
        else
        {
            colliderOfCollision.offset = new Vector2(offset_x_minus, colliderOfCollision.offset.y);
        }   // left
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)    // Player(Layer)
                                                // hit Player
        {
            player_script = collision.gameObject.GetComponent<PlayerController>();
            if (collision.gameObject != null) { player_script.Hit(transform, bandit.GetComponent<Boss_Bandit>().attackPower); }
            else { bandit_script.OffHitBox2D(); }
        }
    }

}
