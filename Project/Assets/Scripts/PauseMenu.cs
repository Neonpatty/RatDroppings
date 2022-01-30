using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


    public class PauseMenu : MonoBehaviour
{ 
    //VARS
    public bool gameIsPaused;
    public GameObject pauseMenuUI;
    public GameObject pauseMenuButtons;
    //public GameObject optionsMenuButtons;
    public Animator pauseMenuAnimation;
    public Animator mainUIAnimation;

    public void Resume()
    {
        //optionsMenuButtons.SetActive(false);
        pauseMenuButtons.SetActive(true);
        pauseMenuAnimation.SetBool("PauseMenuIn", false);
        mainUIAnimation.SetBool("IsActive", false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        pauseMenuAnimation.SetBool("PauseMenuIn", true);
        mainUIAnimation.SetBool("IsActive", true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    //public void Options()
    //{
    //    pauseMenuButtons.SetActive(false);
    //    //optionsMenuButtons.SetActive(true);
    //}

    public void MainMenu()
    {
        Debug.Log("Loading Main Menu");
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }

    //public void Back()
    //{
    //    optionsMenuButtons.SetActive(false);
    //    pauseMenuButtons.SetActive(true);
    //}
}
