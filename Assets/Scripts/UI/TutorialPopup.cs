using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPopup : MonoBehaviour
{
    public TMP_Text keys1, keys2, keys3;

    private void OnEnable()
    {
        HoleTrigger.trigger += GotIt;
    }

    private void OnDisable()
    {
        HoleTrigger.trigger -= GotIt;
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey("tutorial"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            Keybindings keybindings = FindObjectOfType<InputManager>().keybindings;
            keys1.text = keybindings.powerDown.ToString() + " / " + keybindings.powerUp.ToString();
            keys2.text = keybindings.strike.ToString();
            keys3.text = keybindings.pause.ToString();

            PauseMenu.AbilityToPause(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) GotIt();
    }

    public void GotIt()
    {
        PlayerPrefs.SetString("tutorial", "done");
        PlayerPrefs.Save();

        PauseMenu.AbilityToPause(true);

        gameObject.SetActive(false);
    }
}
