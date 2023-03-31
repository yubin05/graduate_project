using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bandit : Boss
{
    // hitbox
    static GameObject hitbox_bandit;   // Bandit's attack collider

    // attack motion
    bool attacking = false;

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
    }

    protected override void Anim_Control()
    {
        base.Anim_Control();
        animator.SetBool("isAttacking", attacking);
    }

    public override void Attack()
    {
        base.Attack();
        if (distanceAbsDifferenceOfPlayer <= 2f)
        {
            rigid.velocity = Vector2.zero;  // stop
            attacking = true;  animator.SetTrigger("attack_trigger");
            OnHitBox2D();   // attack trigger box
        }
    }

    void OnHitBox2D()
    {
        hitbox_bandit.SetActive(true);

        // delay
        StartCoroutine(OffHitBox2D());
    }

    IEnumerator OffHitBox2D()
    {
        yield return new WaitForSeconds(0.1f);
        attacking = false;
        hitbox_bandit.SetActive(false);
    }
}
