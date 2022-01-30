using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
namespace JFMenuTests
{
    public class MultiNameInput : MonoBehaviour
    {
        public Button BackButton;
        public Image Back;

        //Player 1 public objects
        public Image Image1;
        public Image Image2;
        public Image Image3;
        public Image Ready1;

        public TextMeshProUGUI P1Letter1;
        public TextMeshProUGUI P1Letter2;
        public TextMeshProUGUI P1Letter3;

        public TextMeshProUGUI P1Name;

        //Player 2 public objects
        public Image Image4;
        public Image Image5;
        public Image Image6;
        public Image Ready2;

        public TextMeshProUGUI P2Letter1;
        public TextMeshProUGUI P2Letter2;
        public TextMeshProUGUI P2Letter3;

        public TextMeshProUGUI P2Name;

        //variables
        int ActiveNumber;
        int ActiveNumber2;

        bool P1Ready;
        bool P2Ready;

        List<string> Letters = new List<string>
    {
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
    };

        //Player 1 letter indexes
        int i1;
        int i2;
        int i3;

        //Platyer 2 letter indexes
        int i4;
        int i5;
        int i6;

        // Start is called before the first frame update
        void Start()
        {
            ActiveNumber = 1;
            i1 = 0;
            i2 = 0;
            i3 = 0;

            ActiveNumber2 = 1;
            i4 = 0;
            i5 = 0;
            i6 = 0;
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
                //AudioManager.Instance.Play("InputReaction");

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
                //AudioManager.Instance.Play("InputReaction2");

                if (ActiveNumber == 4)
                {
                    P1Name.text = Letters[i1] + Letters[i2] + Letters[i3];
                    PlayerPrefs.SetString("P1Name", P1Name.text);
                    P1Ready = true;
                }

                if (ActiveNumber == 0)
                {
                    OnBackPressed();
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
                //AudioManager.Instance.Play("InputReaction2");

                ActiveNumber = ActiveNumber + 1;
                if (ActiveNumber >= 5)
                {
                    ActiveNumber = 0;
                }
            }

            //Update script for player 2
            if (ActiveNumber2 == 1)
                Image4.enabled = true;
            else
                Image4.enabled = false;
            if (ActiveNumber2 == 2)
                Image5.enabled = true;
            else
                Image5.enabled = false;
            if (ActiveNumber2 == 3)
                Image6.enabled = true;
            else
                Image6.enabled = false;
            if (ActiveNumber2 == 4)
                Ready2.enabled = true;
            else
                Ready2.enabled = false;

            if (Input.GetKeyUp("up"))
            {
                //AudioManager.Instance.Play("InputReaction");

                if (ActiveNumber2 == 1)
                {
                    i4 = i4 + 1;
                    if (i4 > 25)
                    {
                        i4 = 0;
                    }
                    P2Letter1.text = Letters[i4];
                }
                if (ActiveNumber2 == 2)
                {
                    i5 = i5 + 1;
                    if (i5 > 25)
                    {
                        i5 = 0;
                    }
                    P2Letter2.text = Letters[i5];
                }

                if (ActiveNumber2 == 3)
                {
                    i6 = i6 + 1;
                    if (i6 > 25)
                    {
                        i6 = 0;
                    }
                    P2Letter3.text = Letters[i6];
                }
            }
            if (Input.GetKeyUp("down"))
            {
                //AudioManager.Instance.Play("InputReaction");

                if (ActiveNumber2 == 1)
                {
                    i4 = i4 - 1;
                    if (i4 < 0)
                    {
                        i4 = 25;
                    }
                    P2Letter1.text = Letters[i4];
                }
                if (ActiveNumber2 == 2)
                {
                    i5 = i5 - 1;
                    if (i5 < 0)
                    {
                        i5 = 25;
                    }
                    P2Letter2.text = Letters[i5];
                }
                if (ActiveNumber2 == 3)
                {
                    i6 = i6 - 1;
                    if (i6 < 0)
                    {
                        i6 = 25;
                    }
                    P2Letter3.text = Letters[i6];
                }
            }

            //Confirming name
            if (Input.GetKey(KeyCode.RightControl))
            {
                //AudioManager.Instance.Play("InputReaction2");

                if (ActiveNumber2 == 4)
                {
                    P2Name.text = Letters[i4] + Letters[i5] + Letters[i6];
                    PlayerPrefs.SetString("P2Name", P2Name.text);
                    P2Ready = true;
                }
            }
            if (Input.GetKeyUp("left"))
            {
                //AudioManager.Instance.Play("InputReaction2");

                ActiveNumber2 = ActiveNumber2 - 1;
                if (ActiveNumber2 <= 0)
                {
                    ActiveNumber2 = 4;
                }
            }
            if (Input.GetKeyUp("right"))
            {
                //AudioManager.Instance.Play("InputReaction2");

                ActiveNumber2 = ActiveNumber2 + 1;
                if (ActiveNumber2 >= 5)
                {
                    ActiveNumber2 = 1;
                }
            }

            if (P1Ready == true && P2Ready == true)
            {
                SceneManager.LoadScene("Multiplayer");
            }
        }

        public void OnBackPressed()
        {
            SceneManager.LoadScene("TitleMainMenu");
        }
    }
}