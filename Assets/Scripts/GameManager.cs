using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    GameObject player;
    PlayerController player_script;
    int player_inital_sword_attack_power;
    int player_inital_throw_attack_power;
    int player_inital_health;
    int player_inital_max_health;

    GameObject playerUI;
    GameObject gameOverUI;
    GameObject boss_healthUI;

    private void Awake()
    {
        playerUI = GameObject.FindWithTag("PlayerUI");
        gameOverUI = GameObject.FindWithTag("GameOverUI");
    }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        player_script = player.GetComponent<PlayerController>();
        
        // player stat initalization for restarting
        player_inital_sword_attack_power = player_script.player_sword_attack_power;
        player_inital_throw_attack_power = player_script.player_throw_attack_power;
        player_inital_health = player_script.health;
        player_inital_max_health = player_script.max_health;

        player.SetActive(false);
        playerUI.SetActive(false);
    }

    // this method called by PlayerController.cs
    public void GameOver()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // self scene reload
        ActiveGameOverPanel();
        player.SetActive(false); playerUI.SetActive(false);

        // Boss Health UI inactive
        try
        {
            GameObject boss_healthUI = 
                GameObject.FindWithTag("Boss").transform.Find("Boss_HealthUI").gameObject;
            boss_healthUI.SetActive(false);
        } catch (Exception) { }
    }

    void ActiveGameOverPanel()
    {
        gameOverUI.SetActive(true);
    }

    // this method called by Boss.cs
    public void ActiveStageClearPanelController(GameObject stageManager)
    {
        stageManager.GetComponent<StageManager>().ActiveStageClearPanel();
    }

    // this method called by GameOver.cs
    public void RevivePlayer()
    {
        player.SetActive(true); playerUI.SetActive(true);

        player_script.player_sword_attack_power = player_inital_sword_attack_power;
        player_script.player_throw_attack_power = player_inital_throw_attack_power;
        player_script.health = player_inital_health;
        player_script.max_health = player_inital_max_health;

        player_script.canInput = true;
        player.GetComponentInChildren<PlayerHealthUI>().setHealthUI(player_inital_health);
        player.GetComponentInChildren<PlayerHealthUI>().setMaxHealthUI(player_inital_max_health);
        player_script.Vincible();
    }

    // this method called by other scripts in need of loading main menu scene
    // because prevent overlap DontDestroyOnLoad object
    public void LoadMainMenuScene()
    {
        Destroy(gameOverUI); Destroy(playerUI);
        SceneManager.LoadScene(0);
        Destroy(player); Destroy(gameObject);
    }
}
