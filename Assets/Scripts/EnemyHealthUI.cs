using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : HealthUIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        health_amount = transform.root.GetComponentInChildren<Enemy>().health;
        max_health_amount = transform.root.GetComponentInChildren<Enemy>().max_health;
    }
}
