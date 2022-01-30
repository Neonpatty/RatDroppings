using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public GameObject[] scoreVisuals;

    int score = 0;

    public void IncreaseScore()
    {
        if (score++ < 3)
            scoreVisuals[score - 1].SetActive(true);
    }

    public void Reset()
    {
        score = 0;

        foreach (GameObject score in scoreVisuals)
            score.SetActive(false);
    }
}
