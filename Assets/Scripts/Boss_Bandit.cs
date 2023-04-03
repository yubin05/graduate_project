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

    // dash attack percentage
    private float dash_attack_percentage = 1;

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

        // when boss close player, trigger attacked
        if (distanceAbsDifferenceOfPlayer <= 2f && !attack_triggered) { Attack(); }
        // when player out hitbox, set off attacking
        else if (distanceAbsDifferenceOfPlayer > 3f)
        {
            isStaggered = false;
            attacking_animation = false;

            if (distanceAbsDifferenceOfPlayer > 8f)
            {
                float temp = Random.Range(0.0f, 1.0f);
                if (dash_attack_triggered) { temp = (dash_attack_percentage / 100); }
                if (temp <= (dash_attack_percentage/100)) { Dash_Attack(); }
            }
        }
    }

    protected override void Anim_Control()
    {
        base.Anim_Control();
        animator.SetBool("isAttacking", attacking_animation);
    }

    public override void Attack()
    {
        base.Attack();

        isStaggered = true; // stop during attacking player
        attacking_animation = true;  animator.SetTrigger("attack_trigger");

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            attack_triggered = true;
            OnHitBox2D();
        }
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
        attack_triggered = false;   isStaggered = false;
    }

    private void Dash_Attack()
    {
        // prevent attack pattern && move
        dash_attack_triggered = true; moveSwitch = false; attack_triggered = true;

        // teleport
        render.color = new Color(1, 1, 1, 0.5f);    // effect on
        StartCoroutine(Teleport());
        dash_attack_triggered = false;
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

        moveSwitch = true;
    }
}
