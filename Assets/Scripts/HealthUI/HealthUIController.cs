using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    protected Image healthUI;
    protected int health_amount;
    protected int max_health_amount;

    protected virtual void Start()
    {
        healthUI = GetComponent<Image>();
        //healthUI.fillAmount = 1f;     // fill amount initalize lower class
    }

    // following method controled by other Script
    public virtual void setHealthUI(int newHealth)
    {
        health_amount = newHealth;
        healthUI.fillAmount = (health_amount / (float)max_health_amount);
    }
    public virtual void setMaxHealthUI(int newMaxHealth)
    {
        max_health_amount = newMaxHealth;
        healthUI.fillAmount = (health_amount / (float)max_health_amount);
    }
}
