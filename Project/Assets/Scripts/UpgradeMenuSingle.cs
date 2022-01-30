using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuSingle : MonoBehaviour
{
    //VARS
    public GameObject upgradeMenuUI;
    public Animator mainUIAnimation;
    public GameObject player1Paddle;
    public GameObject player2Paddle;
    public GameObject[] player1ExtendPaddleFills;
    public UpgradeMenuTest2.Upgrades[] upgrades;
    public UpgradeMenuTest2.PlayerStats[] playerStats;
    public List<Image> playerSelect1;
    public List<Image> playerReady;
    public List<Material> playerUpgrades;
    public TextMeshProUGUI[] textFields;

    public bool upgradeMenuActive;
    public bool Player1Ready;

    public int activeUpgrade1;
    public int activeUpgrade2;

    void Start()
    {
        activeUpgrade1 = 0;
        activeUpgrade2 = 0;
        textFields[1].text = DMGeneralPrototype.GameManager.instance.player1Score.ToString();
        textFields[2].text = PlayerPrefs.GetString("P1Name");
    }

    void Update()
    {
        //PLAYER ONE SELECTION
        if (activeUpgrade1 == 0)
        {
            playerSelect1[0].enabled = true;
            textFields[0].text = upgrades[0].description;
        }
        else
            playerSelect1[0].enabled = false;

        if (activeUpgrade1 == 1)
        {
            playerSelect1[1].enabled = true;
            textFields[0].text = upgrades[1].description;
        }
        else
            playerSelect1[1].enabled = false;

        if (activeUpgrade1 == 2)
            playerSelect1[2].enabled = true;
        else
            playerSelect1[2].enabled = false;

        //PLAYER ONE CONTROLS
        if (Input.GetKeyDown(KeyCode.A) && upgradeMenuActive)
        {
            activeUpgrade1 = activeUpgrade1 - 1;
            if (activeUpgrade1 < 0)
                activeUpgrade1 = 2;
        }

        if (Input.GetKeyDown(KeyCode.D) && upgradeMenuActive)
        {
            activeUpgrade1 = activeUpgrade1 + 1;
            if (activeUpgrade1 > 2)
                activeUpgrade1 = 0;
        }

        if (Input.GetKeyDown(KeyCode.E) && upgradeMenuActive)
        {
            if (activeUpgrade1 == 0)
                player1Paddle.GetComponent<DMGeneralPrototype.Paddle>().upgrade = upgrades[0];
            else if (activeUpgrade1 == 1)
            {
                foreach (GameObject obj in player1ExtendPaddleFills)
                    obj.SetActive(false);

                player1Paddle.GetComponent<DMGeneralPrototype.Paddle>().IncreasePaddleSize();
                for (int i = 0; i < player1Paddle.GetComponent<DMGeneralPrototype.Paddle>().GetPaddleSize(); i++)
                    player1ExtendPaddleFills[i].SetActive(true);
            }
            else if (activeUpgrade1 == 2)
            {
                if (Player1Ready)
                {
                    Player1Ready = false;
                    playerReady[0].enabled = false;
                }
                else
                {
                    Player1Ready = true;
                    playerReady[0].enabled = true;
                }
            }
        }

        if (Player1Ready && upgradeMenuActive)
            Close();
    }

    //SET UPGRADE MENU ENABLED
    public void Open()
    {
        mainUIAnimation.SetBool("IsActive", true);
        upgradeMenuUI.SetActive(true);
        upgradeMenuActive = true;
        Player1Ready = false;
        playerReady[0].enabled = false;
        activeUpgrade1 = 0;
        Time.timeScale = 0f;
    }

    //SET UPGRADE MENU DISABLED 
    public void Close()
    {
        mainUIAnimation.SetBool("IsActive", false);
        upgradeMenuUI.SetActive(false);
        upgradeMenuActive = false;
        Time.timeScale = 1f;
        DMGeneralPrototype.GameManager.instance.Continue();
        player1Paddle.GetComponent<DMGeneralPrototype.Paddle>().ResetPaddle();
        player2Paddle.GetComponent<DMGeneralPrototype.Paddle>().ResetPaddle();
    }
}