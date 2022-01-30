using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSelection : MonoBehaviour
{
    public void OnMultiplayerPlayerClick()
    {
        SceneManager.LoadScene("MultiSetup");
    }
    public void OnSinglePlayerClick()
    {
        SceneManager.LoadScene("SingleSetup");
    }
    public void OnExitClick()
    {
        Application.Quit();
    }
}
