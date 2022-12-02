using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KeybindingsSave
{
    public KeyCode powerUp, powerDown, strike, pause;

    public KeybindingsSave(Keybindings keybindings)
    {
        powerUp = keybindings.powerUp;
        powerDown = keybindings.powerDown;
        strike = keybindings.strike;
        pause = keybindings.pause;
    }
}
