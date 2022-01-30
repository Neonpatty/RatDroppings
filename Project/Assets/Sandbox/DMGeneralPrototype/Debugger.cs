using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Debugger : MonoBehaviour
{
    public static Debugger Instance
    {
        get
        {
            if (instance == null)
                new GameObject("Debugger").AddComponent<Debugger>();

            return instance;
        }
    }

    static Debugger instance;

    public GameObject debugScreen;
    public GameObject debugPrefab;

    Dictionary<string, TMP_Text> debugObjects = new Dictionary<string, TMP_Text>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
            debugScreen.SetActive(!debugScreen.activeSelf);
    }

    public void DebugInfo(string reference, string text)
    {
        if (!debugObjects.ContainsKey(reference))
        {
            TMP_Text debugObject = Instantiate(debugPrefab, debugScreen.transform).GetComponent<TMP_Text>();
            debugObject.text = text;

            debugObjects.Add(reference, debugObject);

            return;
        }

        debugObjects[reference].text = $"{reference}: {text}";
    }

    public void RemoveInfo(string reference)
    {
        Destroy(debugObjects[reference].gameObject);
        debugObjects.Remove(reference);
    }
}
