using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxController : MonoBehaviour
{
    // hit box controller
    BoxCollider2D colliderOfCollision;

    // player
    GameObject player;
    PlayerController playerController;
    SpriteRenderer player_render;
    
    private float offset_x_plus;
    private float offset_x_minus;

    // Enemy & Boss
    Enemy enemy; Boss boss;

    private void Start()
    {
        colliderOfCollision = gameObject.GetComponent<BoxCollider2D>();

        player = transform.root.gameObject;
        player_render = player.GetComponent<SpriteRenderer>();
        offset_x_plus = colliderOfCollision.offset.x;
        offset_x_minus = offset_x_plus * (-1);

        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (!player_render.flipX)
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
        if (collision.gameObject.layer == 8)    // Enemy(Layer)
            // hit enemy
        {
            enemy = collision.gameObject.GetComponent<Enemy>();
            enemy.Hit(playerController.player_sword_attack_power);
        }
        if (collision.gameObject.layer == 11)    // Boss(Layer)
            // hit boss
        {
            boss = collision.gameObject.GetComponent<Boss>();
            boss.Hit(playerController.player_sword_attack_power);
        }
    }
}
