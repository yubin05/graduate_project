using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bandit : Boss
{
    // hitbox
    static GameObject hitbox_bandit;   // Bandit's attack collider

    // attack motion
    public bool attacking = false;
    bool attack_triggered = false;      // variable for prevent keeping hitbox

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
            attacking = false;
        }
    }

    protected override void Anim_Control()
    {
        base.Anim_Control();
        animator.SetBool("isAttacking", attacking);
    }

    public override void Attack()
    {
        base.Attack();

        isStaggered = true; // stop during attacking player
        attacking = true;  animator.SetTrigger("attack_trigger");

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

        attacking = false;
        StartCoroutine(AfterAttackDelay());
    }

    IEnumerator AfterAttackDelay()
    {
        yield return new WaitForSeconds(1f);
        attack_triggered = false;   isStaggered = false;
    }
}
