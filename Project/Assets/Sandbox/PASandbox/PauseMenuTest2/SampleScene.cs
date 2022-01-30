using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PauseMenuTest2
{
    public class SampleScene : MonoBehaviour
    {
        public void LoadScene()
        {
            SceneManager.LoadScene("PauseMenuTest2");
        }
    }

}
