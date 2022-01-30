using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainUI : MonoBehaviour
{
    public TextMeshProUGUI[] names;

    private void Start()
    {
        SetName();
    }

    public void SetName()
    {
        names[0].text = (PlayerPrefs.GetString("P1Name"));
        names[1].text = (PlayerPrefs.GetString("P2Name"));
    }

}
