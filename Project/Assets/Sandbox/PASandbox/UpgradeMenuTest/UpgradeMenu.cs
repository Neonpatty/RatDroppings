using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UpgradeMenuTest
{
    public class UpgradeMenu : MonoBehaviour
    {
        //VARS
        public bool roundEnded;
        public Animator upgradeMenuAnimation;
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (roundEnded)
                {
                    StartNewRound();
                }
                else
                {
                    TestNewRound();
                }
            }
        }

        public void StartNewRound()
        {

            roundEnded = false;
        }

        public void TestNewRound()
        {
            roundEnded = true;
        }
    }
}

