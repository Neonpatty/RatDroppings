using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PauseMenuTest
{
    public class PauseAnimation : MonoBehaviour
    {
        //VARS
        public GameObject[] objectsToDisable;

        public void Disable()
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(false);
            }
        }

        public void Enable()
        {
            foreach (GameObject obj in objectsToDisable)
            {
                obj.SetActive(true);
            }
        }
    }
}
