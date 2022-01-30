using UnityEngine;
using System.Collections.Generic;


    public struct ScoreEntry
    {
        public string name;
        public int score;

        public ScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    public static class LeaderBoard
    {
        public static List<ScoreEntry> Entries
        {
            get
            {
                if (entries == null || entries.Count == 0)
                {
                    entries = new List<ScoreEntry>();
                    LoadScores();
                }

                return entries;
            }
        }

        const string PlayerPrefsBaseKey = "leaderboard";

        static List<ScoreEntry> entries = new List<ScoreEntry>();

        static void SortScores()
        {
            entries.Sort((a, b) => b.score.CompareTo(a.score));
        }

        static void LoadScores()
        {
            entries.Clear();

            for (int i = 0; i < PlayerPrefs.GetInt("entryCount"); ++i)
            {
                ScoreEntry entry;
                entry.name = PlayerPrefs.GetString($"{PlayerPrefsBaseKey}[{i}].name", "");
                entry.score = PlayerPrefs.GetInt($"{PlayerPrefsBaseKey}[{i}].score", 0);
                entries.Add(entry);
            }
        }

        static void SaveScores()
        {
            SortScores();


            int i = 0;
            foreach (ScoreEntry entry in entries)
            {
                PlayerPrefs.SetString($"{PlayerPrefsBaseKey}[{i}].name", entry.name);
                PlayerPrefs.SetInt($"{PlayerPrefsBaseKey}[{i}].score", entry.score);

                i++;
            }
        }

        public static ScoreEntry GetEntry(int index)
        {
            entries.Clear();

            return Entries[index];
        }

        public static List<ScoreEntry> GetEntries()
        {
            entries.Clear();

            return Entries;
        }

        public static void CreateRecord(string playerName, int playerScore)
        {
            PlayerPrefs.SetInt("entryCount", PlayerPrefs.GetInt("entryCount") + 1);

            entries.Add(new ScoreEntry(playerName, playerScore));

            SaveScores();
        }

        public static void ClearRecords()
        {
            for (int i = 0; i < PlayerPrefs.GetInt("entryCount"); i++)
            {
                PlayerPrefs.DeleteKey($"{PlayerPrefsBaseKey}[{i}].name");
                PlayerPrefs.DeleteKey($"{PlayerPrefsBaseKey}[{i}].score");
            }

            PlayerPrefs.SetInt("entryCount", 0);
        }
    }