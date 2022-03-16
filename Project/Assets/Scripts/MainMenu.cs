using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool clearPrefs;

    private void Awake()
    {
        if (clearPrefs)
            PlayerPrefs.DeleteAll();

        Time.timeScale = 1f;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnMultiplayerPlayerClick()
    {
        SceneManager.LoadScene("PlayerSetupMultiplayer");
        PlayerPrefs.SetString("mode", "multi");
    }

    public void OnSinglePlayerClick()
    {
        SceneManager.LoadScene("PlayerSetupSingleplayer");
        PlayerPrefs.SetString("mode", "single");
        PlayerPrefs.SetString("P2Name", "AI");
    }

    public void OnExitClick()
    {
        Application.Quit();
        Debug.Log("Exiting Game.");
    }

    public void OnLeaderBoardClick()
    {
        SceneManager.LoadScene("Leaderboard");
    }

    public void OnSceneSelectClick()
    {
        SceneManager.LoadScene("QuickSceneSelect");
    }
}