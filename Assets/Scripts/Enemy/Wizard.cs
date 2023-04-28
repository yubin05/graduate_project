using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        isRightDefaultValue = false;
    }

    public override void Move()
    {
        // don't move left and right

        // prevent collider bug
        transform.position = new Vector2(transform.position.x - 0.01f, transform.position.y);
        transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
    }
}
