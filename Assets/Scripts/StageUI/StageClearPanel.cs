using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClearPanel : MonoBehaviour
{
    protected GameObject player;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        player = GameObject.FindWithTag("Player");
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            player.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
