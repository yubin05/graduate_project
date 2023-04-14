using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : HealthUIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        health_amount = GameObject.FindWithTag("Enemy").GetComponent<Enemy>().health;
        max_health_amount = GameObject.FindWithTag("Enemy").GetComponent<Enemy>().max_health;
    }
}
