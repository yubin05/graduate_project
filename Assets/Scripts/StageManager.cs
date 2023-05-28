using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    GameObject stageClearPanel;
    GameObject player;

    // Start is called before the first frame update
    void Awake()
    {
        stageClearPanel = GameObject.FindWithTag("StageClearPanel");
        //stageClearPanel.SetActive(false);     // this method execute by stageClearPanel script

        player = GameObject.FindWithTag("Player");
    }

    // this method called by Boss.cs
    public void ActiveStageClearPanel()
    {
        stageClearPanel.SetActive(true);
        player.SetActive(false);
    }
}
