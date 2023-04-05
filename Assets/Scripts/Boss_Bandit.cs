using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bandit : Boss
{
    // hitbox
    static GameObject hitbox_bandit;   // Bandit's attack collider

    bool attacking_animation = false;      // attack motion
    bool attack_triggered = false;      // variable for prevent keeping hitbox

    bool dash_attack_triggered = false; // variable for tie dash_attack_percent and prevent keeping dash_attack() method

    bool jump_animation = false;
    bool jump_attack_triggered = false; // prevent keeping jump_attack() method

    private int dash_attack_percentage = 10;
    private int jump_attack_percentage = 5;

    private int jump_power = 500;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        moveSpeed = 2;

        // hit box default value <- false(InActive)
        hitbox_bandit = GetComponentInChildren<HitBoxControllor_Bandit>().gameObject;
        hitbox_bandit.SetActive(false);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        // Player detecting
        if (rightThanPlayerX)
        {
            render.flipX = false;
        }
        else
        {
            render.flipX = true;
        }

        int temp = Random.Range(1, 1001);   // all attack trigger percentage
        // bandit sometimes trigger dash_attack
        if (!jump_attack_triggered && temp <= jump_attack_percentage)
        {
            SetOnTriggers();
            Jump_Attack();
        }
        // when bandit jump high, trigger down attack
        if (rigid.velocity.y < -3f)
        {
            isStaggered = true;
            rigid.velocity = new Vector2(rigid.velocity.x, -100f);
        }
        
        // when boss close player, trigger attacked
        if (distanceAbsDifferenceOfPlayer <= 2f)
        {
            if (!attack_triggered)
            {
                SetOnTriggers();
                Attack();
            }
        }
        // when player out hitbox, set off attacking
        else if (distanceAbsDifferenceOfPlayer > 2f)
        {
            isStaggered = false;
            attacking_animation = false;    attack_triggered = false;

            if (distanceAbsDifferenceOfPlayer > 8f)
            {
                if (!dash_attack_triggered && temp <= (dash_attack_percentage)) { Dash_Attack(); }
            }
        }
    }

    protected override void Anim_Control()
    {
        base.Anim_Control();

        animator.SetBool("isAttacking", attacking_animation);      
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            OnHitBox2D();
        }

        animator.SetBool("isJumpAttacking", jump_animation);
        if (jump_animation) { animator.Play("Jump"); }
    }

    public override void Attack()
    {
        base.Attack();

        isStaggered = true; // stop during attacking player
        attacking_animation = true;  animator.SetTrigger("attack_trigger");
    }

    void OnHitBox2D()
    {
        hitbox_bandit.SetActive(true);
        StartCoroutine(OffHitBox2D());
    }

    public IEnumerator OffHitBox2D()
    {
        yield return new WaitForSeconds(0.1f);
        hitbox_bandit.SetActive(false);

        attacking_animation = false;
        StartCoroutine(AfterAttackDelay());
    }

    IEnumerator AfterAttackDelay()
    {
        yield return new WaitForSeconds(1f);
        SetOffTriggers();   isStaggered = false;
    }

    private void Dash_Attack()
    {
        // prevent attack pattern && move
        SetOnTriggers(); moveSwitch = false;

        // teleport
        render.color = new Color(1, 1, 1, 0.5f);    // effect on
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1f);

        float position_offset_x = 1.5f;
        if (render.flipX) { position_offset_x *= -1f; }
        transform.position = new Vector2(player.transform.position.x + position_offset_x, transform.position.y);

        render.color = new Color(1, 1, 1, 1);   // effect off

        // attacking
        attack_triggered = false;

        // after a while other constraints set off
        StartCoroutine(AfterTeleportSetOffConstrarints());
    }

    IEnumerator AfterTeleportSetOffConstrarints()
    {
        yield return new WaitForSeconds(1f);

        dash_attack_triggered = false; jump_attack_triggered = false; moveSwitch = true;
    }

    private void Jump_Attack()
    {
        jump_animation = true;  animator.SetTrigger("jump_trigger");
        rigid.AddForce(new Vector2(moveDirection * distanceAbsDifferenceOfPlayer * 300, jump_power));
        StartCoroutine(AfterSetOffJumpAttackConstrarints());
    }

    IEnumerator AfterSetOffJumpAttackConstrarints()
    {
        yield return new WaitForSeconds(1f);
        SetOffTriggers();   jump_animation = false;
    }

    // setting all trigger related attack because prevent other attack pattern while one attack pattern
    void SetOnTriggers()
    {
        attack_triggered = true; dash_attack_triggered = true; jump_attack_triggered = true;
    }
    void SetOffTriggers()
    {
        attack_triggered = false; dash_attack_triggered = false; jump_attack_triggered = false;
    }
}
