using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthUI : HealthUIController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        health_amount = transform.root.GetComponent<Boss>().health;
        max_health_amount = transform.root.GetComponent<Boss>().max_health;

        healthUI.fillAmount = (health_amount / (float)max_health_amount);
    }
}
