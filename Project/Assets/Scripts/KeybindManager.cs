using System;
using System.Collections.Generic;
using UnityEngine;

public class KeybindManager : MonoBehaviour
{
    public static KeybindManager Instance { get; private set; }

    public List<string> defaultValueKeys = new List<string>();
    public List<string> defaultValues = new List<string>();

    List<Keybinder> keybinders = new List<Keybinder>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);

        DontDestroyOnLoad(gameObject);
    }

    public void ActivateKeybinder(Keybinder keybinder)
    {
        foreach (Keybinder binder in keybinders)
            binder.DeactivateKeybinder();

        keybinder.ActivateKeybinder();
    }

    public void AddKeybinder(Keybinder binder)
    {
        if (!keybinders.Contains(binder))
            keybinders.Add(binder);
    }

    public KeyCode GetKeybind(string keyCode)
    {
        return (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString(keyCode, defaultValues[defaultValueKeys.IndexOf(keyCode)]));
    }
}
