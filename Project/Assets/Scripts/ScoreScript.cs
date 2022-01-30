using UnityEngine;
using System.Collections.Generic;

public class ScoreScript : MonoBehaviour
{
    public GameObject textPrefab;

    public Transform scoresParent;

    List<GameObject> gameObjects = new List<GameObject>();

    private void Start()
    {
        Populate();
    }

    public void Populate()
    {
        foreach (GameObject obj in gameObjects)
        {
            Destroy(obj);
        }

        List<ScoreEntry> entries = LeaderBoard.GetEntries();

        for (int i = 0; i < entries.Count; i++)
        {
            ScoreEntry entry = entries[i];

            GameObject newObj = Instantiate(textPrefab, scoresParent);

            LeaderboardScore script = newObj.GetComponent<LeaderboardScore>();
            script.playerName.text = entry.name;
            script.playerScore.text = entry.score.ToString("0000");
            script.playerRank.text = (i + 1).ToString("000");

            gameObjects.Add(newObj);
        }
    }

    public void Clear()
    {
        LeaderBoard.ClearRecords();

        Populate();
    }

    string GenerateStringName(int length)
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        char[] stringChars = new char[length];
        System.Random random = new System.Random(Random.Range(0, 100000));

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        string finalString = new string(stringChars);

        return finalString;
    }
}
