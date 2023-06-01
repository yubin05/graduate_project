using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHealthPack : ItemPack
{
    protected override void TriggeredItem(GameObject player, PlayerController player_script)
    {
        player_script.max_health += addValue;
        player.GetComponentInChildren<PlayerHealthUI>().setMaxHealthUI(player_script.max_health);    // player health UI control
    }
}
