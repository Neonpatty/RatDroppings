using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace PauseMenuTest2
{
    public class PauseMenu : MonoBehaviour
    {
        //VARS
        public bool gameIsPaused;
        public GameObject pauseMenuUI;
        public GameObject pauseMenuButtons;
        public GameObject optionsMenuButtons;
        public Animator pauseMenuAnimation;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (gameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }    
        }

        public void Resume()
        {
            optionsMenuButtons.SetActive(false);
            pauseMenuButtons.SetActive(true);
            pauseMenuAnimation.SetBool("PauseMenuIn", false);
            gameIsPaused = false;
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            pauseMenuAnimation.SetBool("PauseMenuIn", true);
            gameIsPaused = true;
            Time.timeScale = 0f;
        }

        public void Options()
        {
            pauseMenuButtons.SetActive(false);
            optionsMenuButtons.SetActive(true);
        }

        public void MainMenu()
        {
            Debug.Log("Loading Main Menu");
            SceneManager.LoadScene("SampleScene1");
        }

        public void ExitGame()
        {
            Debug.Log("Exiting Game");
            Application.Quit();
        }

        public void Back()
        {
            optionsMenuButtons.SetActive(false);
            pauseMenuButtons.SetActive(true);
        }
    }
}
