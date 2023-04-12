using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rino : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 1;
        isRightDefaultValue = false;
    }
}
