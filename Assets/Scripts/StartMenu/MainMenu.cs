using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] public GameObject gameManager;
    private GameObject playerUI;

    private void Start()
    {
        playerUI = GameObject.FindWithTag("PlayerUI");
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
        DontDestroyOnLoad(playerUI);
        DontDestroyOnLoad(gameManager);
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
