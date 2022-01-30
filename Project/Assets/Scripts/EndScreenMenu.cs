using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenMenu : MonoBehaviour
{
    public GameObject endScreenMenuUI;

    public Animator endScreenAnimation;
    public Animator mainUIAnimation;

    bool ended;

    float timer;

    private void Update()
    {
        if (ended)
            timer += Time.unscaledDeltaTime;

        if (timer >= 3.0f)
            SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
        ended = true;
        endScreenMenuUI.SetActive(true);
        endScreenAnimation.SetBool("IsActive", false);
        mainUIAnimation.SetBool("IsActive", true);

        Time.timeScale = 0f;
    }
}
