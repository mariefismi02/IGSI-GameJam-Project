using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{

    private AudioSource audioClick;
    public GameObject panelQuit;
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.Escape) && panelQuit!=null)
        {
            panelQuit.SetActive(true);
        }
        audioClick = GetComponent<AudioSource>();
        
    }

    public void GoToGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void CancelExit()
    {
        panelQuit.SetActive(false);
    }
}
