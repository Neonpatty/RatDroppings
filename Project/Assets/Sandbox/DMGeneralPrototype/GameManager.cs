using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.SceneManagement;

namespace DMGeneralPrototype
{

    [System.Serializable]
    public struct DroneSettings
    {
        public Vector2 topLeftCap;
        public Vector2 bottomRightCap;

        public float spawnChance;

        public float verticalOffset;
        public Vector3 centerSpawnDist;

        public GameObject dronePrefab;

    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        public bool soloGame = false;

        public UnityEvent gameStartEvent;
        public UnityEvent resetEvent;
        public UnityEvent resetRoundEvent;
        public UnityEvent player1Scored;
        public UnityEvent player2Scored;

        public TMP_Text player1UpgradePointsText;
        public TMP_Text player2UpgradePointsText;
        public TMP_Text roundCounter;
        public TMP_Text winnerText;

        public TMP_Text startTimerText;

        public GameObject upgradeScreen;
        public GameObject winScreen;

        public PauseMenu pauseMenu;

        [Space]
        public DroneSettings droneSettings;

        [HideInInspector]
        public int player1Score = 0;
        [HideInInspector]
        public int player2Score = 0;

        [HideInInspector]
        public int player1Wins = 0;
        [HideInInspector]
        public int player2Wins = 0;

        int player1UpgradePoints = 0;
        int player2UpgradePoints = 0;
        int round = 1;

        float startTimer = 3.0f;

        bool started = false;
        bool paused = false;
        bool gamePause = false;

        private void Awake()
        {
            //Assign GameManager instance
            if (instance == null)
                instance = this;
            else
                Destroy(this);

            soloGame = PlayerPrefs.GetString("mode") == "single";
        }

        private void Start()
        {
            roundCounter.text = soloGame ? player1Wins.ToString() : round.ToString();

            Debugger.Instance.DebugInfo("Wins", $"Wins: {player1Wins}");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                gamePause = !gamePause;

                if (gamePause)
                    pauseMenu.Pause();
                else
                    pauseMenu.Resume();
            }

            if (paused || gamePause)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            if (!started && !paused)
            {
                startTimer -= Time.deltaTime;

                if (startTimer <= 0.0f)
                {
                    started = true;
                    gameStartEvent.Invoke();
                }
            }

            startTimerText.text = Mathf.CeilToInt(startTimer).ToString("0");

            if (Input.GetKeyDown(KeyCode.F2))
            {
                resetEvent.Invoke();
                started = false;
                startTimer = 3.0f;

                for (int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }

            if (Input.GetKeyDown(KeyCode.F1))
                gameStartEvent.Invoke();
        }

        public void AddUpgradePoint(int player)
        {
            if (player == 1)
            {
                player1UpgradePoints++;
                player1UpgradePointsText.text = player1UpgradePoints.ToString("00");
            }
            else if (player == 2)
            {
                player2UpgradePoints++;
                player2UpgradePointsText.text = player2UpgradePoints.ToString("00");
            }
        }

        public void SpawnUpgradePoint()
        {
            if (Random.Range(0, 100) < droneSettings.spawnChance)
            {
                float xPos = Random.Range(droneSettings.topLeftCap.x, droneSettings.bottomRightCap.x);
                float yPos = Random.Range(droneSettings.bottomRightCap.y, droneSettings.topLeftCap.y);

                Vector3 pointSpawn = new Vector3(xPos, droneSettings.verticalOffset, yPos);

                GameObject newDrone = Instantiate(droneSettings.dronePrefab, GetDroneSpawn(), Quaternion.identity, transform);
                newDrone.GetComponent<Drone>().SetTarget(pointSpawn, GetDroneSpawn(), transform);
            }
        }

        Vector3 GetDroneSpawn()
        {
            int spawnArea = Random.Range(0, 4);

            Vector3 spawnPos = Vector3.up * droneSettings.centerSpawnDist.y;

            switch (spawnArea)
            {
                case 0:
                    spawnPos.x = Random.Range(-droneSettings.centerSpawnDist.x, droneSettings.centerSpawnDist.x);
                    spawnPos.z = droneSettings.centerSpawnDist.z;
                    break;
                case 1:
                    spawnPos.x = Random.Range(-droneSettings.centerSpawnDist.x, droneSettings.centerSpawnDist.x);
                    spawnPos.z = -droneSettings.centerSpawnDist.z;
                    break;
                case 2:
                    spawnPos.x = droneSettings.centerSpawnDist.x;
                    spawnPos.z = Random.Range(-droneSettings.centerSpawnDist.z, droneSettings.centerSpawnDist.z);
                    break;
                case 3:
                    spawnPos.x = -droneSettings.centerSpawnDist.x;
                    spawnPos.z = Random.Range(-droneSettings.centerSpawnDist.z, droneSettings.centerSpawnDist.z);
                    break;

                default:
                    break;
            }

            return spawnPos;
        }

        public void DeleteDrone(GameObject droneToDelete)
        {
            Destroy(droneToDelete);
        }

        public void ResetGame()
        {
            resetEvent.Invoke();
            started = false;
            startTimer = 3.0f;

            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }

        public void CheckGameState()
        {
            if (player1Score >= 3)
            {
                player1Wins++;

                if (soloGame)
                {
                    RoundComplete();
                }
            }
            else if (player2Score >= 3)
            {
                player2Wins++;

                if (soloGame)
                {
                    winnerText.text = $"Your Score is {player1Wins}";
                    winScreen.GetComponent<EndScreenMenu>().EndGame();
                    paused = true;
                    JNSandbox.LeaderBoard.CreateRecord(PlayerPrefs.GetString("P1Name"), player1Wins);
                }
            }

            if (player1Wins < 2 && player2Wins < 2 && (player1Score >= 3 || player2Score >= 3))
                RoundComplete();

            if (soloGame)
            {
                return;
            }

            if (player1Wins >= 2)
            {
                winnerText.text = PlayerPrefs.GetString("P1Name");
                winScreen.GetComponent<EndScreenMenu>().EndGame();
                paused = true;
            }
            else if (player2Wins >= 2)
            {
                winnerText.text = PlayerPrefs.GetString("P2Name");
                winScreen.GetComponent<EndScreenMenu>().EndGame();
                paused = true;
            }
        }

        public void RoundComplete()
        {
            started = false;
            if (!soloGame)
                upgradeScreen.GetComponent<UpgradeMenuTest2.UpgradeMenu>().Open();
            else
                upgradeScreen.GetComponent<UpgradeMenuSingle>().Open();
            paused = true;
            Time.timeScale = 0.0f;
        }

        public void Continue()
        {
            paused = false;
            resetRoundEvent.Invoke();
            player1Score = 0;
            player2Score = 0;
            round++;
            roundCounter.text = soloGame ? player1Wins.ToString() : round.ToString();
            Debugger.Instance.DebugInfo("Wins", $"{player1Wins}");
            ResetGame();
        }

        public void MainMenu()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(3);
        }

        public void ReloadScene()
        {
            Time.timeScale = 1.0f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}