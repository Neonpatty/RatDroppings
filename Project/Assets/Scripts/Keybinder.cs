using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keybinder : MonoBehaviour
{
    public string keyBinding;

    public TMP_Text text;

    public GameObject activeIndicator;

    bool settingKeybind;

    readonly Array keyCodes = Enum.GetValues(typeof(KeyCode));

    private void Awake()
    {
        KeybindManager.Instance.AddKeybinder(this);

        GetComponent<Button>()?.onClick.AddListener(ActivateKeybinder);

        KeyCode currentKey = KeybindManager.Instance.GetKeybind(keyBinding);
        text.text = currentKey.ToString();
    }

    private void Update()
    {
        if (Input.anyKeyDown && settingKeybind)
        {
            foreach (KeyCode keyCode in keyCodes)
            {
                if (Input.GetKey(keyCode))
                {
                    if (keyCode == KeyCode.Mouse0 || keyCode == KeyCode.Escape)
                    {
                        DeactivateKeybinder();
                        return;
                    }

                    SetKey(keyCode);
                }
            }
        }
    }

    public void SetKey(KeyCode key)
    {
        PlayerPrefs.SetString(keyBinding, key.ToString());
        DeactivateKeybinder();
        text.text = key.ToString();
        Debug.Log($"Set `{keyBinding}` to `{key}`");
    }

    public void ActivateKeybinder()
    {
        settingKeybind = true;
        activeIndicator?.SetActive(true);
    }

    public void DeactivateKeybinder()
    {
        settingKeybind = false;
        activeIndicator?.SetActive(false);
    }
}
