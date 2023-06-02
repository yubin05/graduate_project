using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : HealthUIController
{
    Enemy enemy;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        enemy = transform.parent.parent.parent.GetComponent<Enemy>();
        health_amount = enemy.health;
        max_health_amount = enemy.max_health;

        healthUI.fillAmount = (health_amount / (float)max_health_amount);
    }
}
