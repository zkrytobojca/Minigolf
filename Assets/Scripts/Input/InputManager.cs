using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector]
    public static InputManager instance;

    public Keybindings keybindings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        
        if (PlayerPrefs.HasKey("keybindings"))
        {
            string jsonLoad = PlayerPrefs.GetString("keybindings");
            KeybindingsSave keybindingsLoad = JsonUtility.FromJson<KeybindingsSave>(jsonLoad);
            if (keybindingsLoad != null) keybindings.LoadFromSave(keybindingsLoad);
        }
        else
        {
            keybindings = Resources.Load<Keybindings>("ScriptableObjects/Keybindings");
        }
    }

    public bool KeyDown(Keybindings.ControlKey key)
    {
        if (Input.GetKeyDown(keybindings.CheckKey(key))) return true;
        else return false;
    }

    public bool KeyPressed(Keybindings.ControlKey key)
    {
        if (Input.GetKey(keybindings.CheckKey(key))) return true;
        else return false;
    }

    void OnApplicationQuit()
    {
        string jsonSave = JsonUtility.ToJson(new KeybindingsSave(keybindings));
        PlayerPrefs.SetString("keybindings", jsonSave);
        PlayerPrefs.Save();
    }
}
