using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    protected Rigidbody2D rigid;
    protected SpriteRenderer render;
    protected Animator animator;

    // Player Object
    protected GameObject player;

    // AudioManager
    protected GameObject audio_;
    protected AudioManager audioManager;
    protected bool playedDeadSound = false; // play dead audioclip only once

    public int health;
    public int max_health;  // auto setting

    public int attackPower;
    public int contactPower;

    // boss position.x compare player position.x
    protected bool rightThanPlayerX = true;
    protected float distanceAbsDifferenceOfPlayer;

    protected int moveSpeed;
    protected int moveDirection;

    // when hit by player, boss is stagger for a while
    protected bool isStaggered = false;

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        player = GameObject.FindWithTag("Player");
    }

    protected virtual void Start()
    {
        // get audio manager
        audio_ = GameObject.FindWithTag("AudioManager");
        audioManager = audio_.GetComponent<AudioManager>();

        render.flipX = false;
        max_health = health;
    }

    protected virtual void Update()
    {
        Anim_Control();

        // Player detecting
        if (transform.position.x - player.transform.position.x >= 0)
        {
            rightThanPlayerX = true;
        }
        else
        {
            rightThanPlayerX = false;
        }
        distanceAbsDifferenceOfPlayer = Mathf.Abs(transform.position.x - player.transform.position.x);
        
        // Boss move but dead
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) { Move(); }

        // Boss is dead
        if (health <= 0) { Dead(); }
    }

    // update animator variable like executing animation every time
    protected virtual void Anim_Control()
    {
        // move animation
        animator.SetFloat("velocity_x_abs", Mathf.Abs(rigid.velocity.x));
    }

    // move route
    public virtual void Move()
    {
        if (rightThanPlayerX) { moveDirection = -1; }   // left move because player exist left than boss
        else { moveDirection = 1; }                     // right move because player exist light than boss
        
        // when player close boss, boss move to player
        if (distanceAbsDifferenceOfPlayer <= 8f && !isStaggered)
        {
            rigid.velocity = new Vector2(moveDirection * moveSpeed, rigid.velocity.y);
        }

        // if hole exist front, boss turn
        RaycastHit2D hole_check = Physics2D.Raycast(new Vector2(rigid.position.x + (moveDirection / 2.0f), rigid.position.y),
                                                Vector3.down, 1f, LayerMask.GetMask("Floor", "Platform"));
        if (hole_check.collider == null)
        {
            render.flipX = !render.flipX;   // turn
        }
    }

    // when boss detect player and boss attack player
    public virtual void Attack()
    {

    }

    // when boss contact player
    // following method controled by OnCollisionEnter2D of this script
    protected virtual void Contact(Collision2D collision, int contactPower)
    {
        collision.gameObject.GetComponent<PlayerController>().Hit(transform, contactPower);
    }

    // when boss dead
    // you can use this Dead() method directly by others
    public virtual void Dead()
    {
        animator.SetTrigger("OnDead");

        gameObject.layer = 12;   // Boss Dead

        // when other script execute this method directly
        health = 0;
        //gameObject.GetComponentInChildren<EnemyHealthUI>().setHealthUI(0); // set health bar UI

        rigid.velocity = Vector2.zero;  // stop

        // if animation finish, death animation is keeping
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f)
        {
            animator.Play("Death", -1, 0.99999f);
            StartCoroutine(DestroyBoss());
        }
    }

    protected virtual IEnumerator DestroyBoss()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
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

    // when boss hit
    // following method controled by HitBoxController.cs
    public virtual void Hit(int player_attack_power)
    {
        animator.SetBool("isDamaged", true);
        animator.SetTrigger("OnDamaged");

        isStaggered = true;

        // delay animation
        StartCoroutine(OffHitAnimation());

        // health decreased
        health -= player_attack_power;
        //gameObject.GetComponentInChildren<EnemyHealthUI>().setHealthUI(health); // set health bar UI
    }

    public virtual IEnumerator OffHitAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("isDamaged", false);
        isStaggered = false;
    }
}
