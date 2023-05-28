using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public float respawn_cooltime;

    // this method called by PlayerController.cs
    public void Respawn()
    {
        StartCoroutine(GameOverRetry());
    }

    IEnumerator GameOverRetry()
    {
        yield return new WaitForSeconds(respawn_cooltime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);     // self scene reload
    }

    // this method called by Boss.cs
    public void ActiveStageClearPanelController(GameObject stageManager)
    {
        stageManager.GetComponent<StageManager>().ActiveStageClearPanel();
    }
}
