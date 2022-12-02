using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlKeyButton : MonoBehaviour
{
    public Keybindings.ControlKey controlKey;

    private GameObject current;

    private void Awake()
    {
        transform.GetComponentInChildren<TMP_Text>().text = InputManager.instance.keybindings.CheckKey(controlKey).ToString();
    }

    private void OnGUI()
    {
        if (current != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                InputManager.instance.keybindings.ChangeKey(controlKey, e.keyCode);
                transform.GetComponentInChildren<TMP_Text>().text = e.keyCode.ToString();
                current = null;
            }
        }
    }

    public void OnClick()
    {
        current = gameObject;
    }
}
