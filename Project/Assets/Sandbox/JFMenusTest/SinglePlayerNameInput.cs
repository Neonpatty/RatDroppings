using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
namespace JFMenuTests
{
    public class SinglePlayerNameInput : MonoBehaviour
    {
        public Button BackButton;

        //Player 1 public objects
        public Image Image1;
        public Image Image2;
        public Image Image3;
        public Image Ready1;
        public Image Back;

        public TextMeshProUGUI P1Letter1;
        public TextMeshProUGUI P1Letter2;
        public TextMeshProUGUI P1Letter3;

        public TextMeshProUGUI P1Name;

        //variables
        int ActiveNumber;
        bool P1Ready;

        List<string> Letters = new List<string>
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

        //Player 1 letter indexes
        int i1;
        int i2;
        int i3;

        // Start is called before the first frame update
        void Start()
        {
            ActiveNumber = 1;
            i1 = 0;
            i2 = 0;
            i3 = 0;
        }

        // Update is called once per frame
        void Update()
        {
            //Update script for playr 1
            if (ActiveNumber == 1)
                Image1.enabled = true;
            else
                Image1.enabled = false;
            if (ActiveNumber == 2)
                Image2.enabled = true;
            else
                Image2.enabled = false;
            if (ActiveNumber == 3)
                Image3.enabled = true;
            else
                Image3.enabled = false;
            if (ActiveNumber == 4)
                Ready1.enabled = true;
            else
                Ready1.enabled = false;
            if (ActiveNumber == 0)
                Back.enabled = true;
            else
                Back.enabled = false;

            //Control for letter scrolling
            if (Input.GetKeyUp("w"))
            {
                // AudioManager.Instance.Play("InputReaction");

                if (ActiveNumber == 1)
                {
                    i1 = i1 + 1;
                    if (i1 > 25)
                    {
                        i1 = 0;
                    }
                    P1Letter1.text = Letters[i1];
                }
                if (ActiveNumber == 2)
                {
                    i2 = i2 + 1;
                    if (i2 > 25)
                    {
                        i2 = 0;
                    }
                    P1Letter2.text = Letters[i2];
                }

                if (ActiveNumber == 3)
                {
                    i3 = i3 + 1;
                    if (i3 > 25)
                    {
                        i3 = 0;
                    }
                    P1Letter3.text = Letters[i3];
                }
            }
            if (Input.GetKeyUp("s"))
            {
                //AudioManager.Instance.Play("InputReaction");

                if (ActiveNumber == 1)
                {
                    i1 = i1 - 1;
                    if (i1 < 0)
                    {
                        i1 = 25;
                    }
                    P1Letter1.text = Letters[i1];
                }
                if (ActiveNumber == 2)
                {
                    i2 = i2 - 1;
                    if (i2 < 0)
                    {
                        i2 = 25;
                    }
                    P1Letter2.text = Letters[i2];
                }
                if (ActiveNumber == 3)
                {
                    i3 = i3 - 1;
                    if (i3 < 0)
                    {
                        i3 = 25;
                    }
                    P1Letter3.text = Letters[i3];
                }
            }
            //Confirming name
            if (Input.GetKey("e"))
            {
                // AudioManager.Instance.Play("InputReaction2");

                if (ActiveNumber == 4)
                {
                    P1Name.text = Letters[i1] + Letters[i2] + Letters[i3];
                    PlayerPrefs.SetString("P1Name", P1Name.text);
                    P1Ready = true;
                }

                if (ActiveNumber == 0)
                {
                    SceneManager.LoadScene("TitleMainMenu");
                }
            }

            //Control for Active Number P1
            if (Input.GetKeyUp("a"))
            {
                //AudioManager.Instance.Play("InputReaction2");

                ActiveNumber = ActiveNumber - 1;
                if (ActiveNumber <= -1)
                {
                    ActiveNumber = 4;
                }
            }
            if (Input.GetKeyUp("d"))
            {
                // AudioManager.Instance.Play("InputReaction2");

                ActiveNumber = ActiveNumber + 1;
                if (ActiveNumber >= 5)
                {
                    ActiveNumber = 0;
                }
            }

            if (P1Ready == true)
            {
                SceneManager.LoadScene("SingleplayerSandbox");
            }
        }

        public void OnBackPressed()
        {
            SceneManager.LoadScene("TitleMainMenu");
        }
    }
}