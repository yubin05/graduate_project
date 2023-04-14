using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : Enemy
{
    private int defaultMoveSpeed = 2;
    private int runSpeed = 8;

    private int defaultContactPower;
    private bool isDetectedPlayer;

    protected override void Awake()
    {
        base.Awake();
        moveSpeed = defaultMoveSpeed;
        isRightDefaultValue = false;

        // attack damage control
        defaultContactPower = contactPower;
    }

    protected override void Start()
    {
        base.Start();

        // get audio manager
        audioManager = gameObject.GetComponentInChildren<AudioManager_Rino>();
    }

    protected override void Anim_Control()
    {
        base.Anim_Control();

        // move speed variable
        animator.SetInteger("moveSpeed", moveSpeed);
    }

    // Rino's Death animation finish 0.0000001f
    protected override void DestroyEnemyObject()
    {
        //base.DestroyObject();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
        {
            // destroy rino after second
            StartCoroutine(DestroyRinoObject());
        }
    }
    IEnumerator DestroyRinoObject()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

    public override void Attack()
    {
        //base.Attack();

        // rino detect player
        int i;
        for (i=0; i<10; i++)
        {
            RaycastHit2D detect = Physics2D.Raycast(new Vector2(rigid.position.x + (moveDirection / 2.0f), rigid.position.y + (i/5f)),
                                                new Vector2(moveDirection, 0), 8f, LayerMask.GetMask("Player", "PlayerDamaged"));
            //Debug.DrawRay(new Vector2(rigid.position.x + (moveDirection / 2.0f), rigid.position.y + (i/5f)),
            //    new Vector2(moveDirection, 0) * 8f, Color.red);   // raycast test

            if (detect)
            {
                isDetectedPlayer = true;
                break;
            }
        }
        if (i >= 10) { isDetectedPlayer = false; }  // can't detect player

        if (isDetectedPlayer)
        {
            //Debug.Log("플레이어 감지");
            Run();
        }
        else
        {
            NotRun();
        }
    }   
    private void Run()
    {
        moveSpeed = runSpeed;
        contactPower = attackPower;     // rino attack while running
    }
    private void NotRun()
    {
        moveSpeed = defaultMoveSpeed;
        contactPower = defaultContactPower;
    }
}
