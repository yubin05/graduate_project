using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearPanel : StageClearPanel
{
    GameManager gameManager;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            gameManager.LoadMainMenuScene();  // back to main menu
        }
    }
}
