using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        GameObject teleport_points = GameObject.Find("Teleport_Points");
        if (teleport_points)
        { 
            Transform[] allChildren = teleport_points.GetComponentsInChildren<Transform>();

            foreach (Transform child in allChildren)
            {
                // except parent object (== self)
                if (child.name == teleport_points.name) { continue; }

                SpriteRenderer child_sprite = child.GetComponent<SpriteRenderer>();
                child_sprite.color = new Color(1, 1, 1, 0);     // invisible
            }
        }
    }
}
