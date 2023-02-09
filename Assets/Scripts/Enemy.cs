using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer render;
    
    Animator animator;

    // AudioManager
    GameObject audio_;
    protected AudioManager audioManager;
    protected bool playedDeadSound = false; // play dead audioclip only once

    public int health;
    public int max_health;  // auto setting

    public int attackPower;
    public int contactPower;

    protected int moveSpeed;
    private int moveDirection;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        render.flipX = true;
    }

    protected virtual void Start()
    {
        // get audio manager
        audio_ = GameObject.FindWithTag("AudioManager");
        audioManager = audio_.GetComponent<AudioManager>();
    }

    protected virtual void Update()
    {
        // Enemy move but dead
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) { Move(); }

        // Enemy is dead
        if (health <= 0) { Dead(); }
    }

    // move route
    public virtual void Move()
    {
        if (render.flipX) { moveDirection = -1; }   // left
        else { moveDirection = 1; }                 // right
        rigid.velocity = new Vector2(moveDirection * moveSpeed, rigid.velocity.y);

        // if hole exist front, enemy turn
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(rigid.position.x + (moveDirection/2.0f), rigid.position.y),
                                                Vector3.down, 1f, LayerMask.GetMask("Floor", "Platform"));
        if (hit.collider == null)
        {
            render.flipX = !render.flipX;   // turn
        }
    }

    // when enemy detect player and enemy attack player
    public virtual void Attack()
    {

    }

    // when enemy contact player
    // following method controled by OnCollisionEnter2D of this script
    protected virtual void Contact(Collision2D collision, int contactPower)
    {
        collision.gameObject.GetComponent<PlayerController>().Hit(transform, contactPower);
    }

    // when enemy dead
    // you can use this Dead() method directly by others
    public virtual void Dead()
    {
        animator.SetTrigger("OnDead");

        gameObject.layer = 9;   // Enemy Dead

        // when other script execute this method directly
        health = 0;
        gameObject.GetComponentInChildren<EnemyHealthUI>().setHealthUI(0); // set health bar UI

        // if animation finish, this object destroy
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Wall")
            // when enemy contact wall or door
        {
            render.flipX = !render.flipX;   // turn
        }
        else if (collision.transform.tag == "Player")
            // when enemy contact player
        {
            Contact(collision, contactPower);
        }
    }

    public virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.tag == "Floor" || collision.transform.tag == "Platform")
        {
            // set off freeze position Y
            rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    public virtual void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.transform.tag == "Floor")
        {
            // set on freeze position Y
            rigid.constraints =
                RigidbodyConstraints2D.FreezePositionY |
                RigidbodyConstraints2D.FreezeRotation;
        }
    }

    // when enemy hit
    // following method controled by HitBoxController.cs
    public virtual void Hit(int player_attack_power)
    {
        animator.SetBool("isDamaged", true);
        animator.SetTrigger("OnDamaged");

        // delay animation
        StartCoroutine(OffHitAnimation());

        // health decreased
        health -= player_attack_power;
        gameObject.GetComponentInChildren<EnemyHealthUI>().setHealthUI(health); // set health bar UI
    }

    public virtual IEnumerator OffHitAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("isDamaged", false);
    }
}
