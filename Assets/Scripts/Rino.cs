using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 2;
        isRightDefaultValue = false;
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
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public override void Attack()
    {
        //base.Attack();

        // rino detect player
        RaycastHit2D detect = Physics2D.Raycast(new Vector2(rigid.position.x + (moveDirection / 2.0f), rigid.position.y),
                                                new Vector2(moveDirection, 0), 8f, LayerMask.GetMask("Player"));
        Debug.DrawRay(new Vector2(rigid.position.x + (moveDirection / 2.0f), rigid.position.y),
            new Vector2(moveDirection, 0) * 8f, Color.red);   // raycast test
        if (detect) { Debug.Log("플레이어 감지"); }
    }
}
