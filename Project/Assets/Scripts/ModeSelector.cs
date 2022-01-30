using UnityEngine;

public class ModeSelector : MonoBehaviour
{
    void Awake()
    {
        if (PlayerPrefs.GetString("mode") == "single")
            Destroy(GetComponent<DMGeneralPrototype.PlayerPaddle>());
        else
            Destroy(GetComponent<DMGeneralPrototype.AIPaddle>());
    }
}
