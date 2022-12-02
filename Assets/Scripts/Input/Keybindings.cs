using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewKeybindings", menuName = "ScriptableObjects/Keybindings")]
public class Keybindings : ScriptableObject
{
    public enum ControlKey
    {
        PowerUp,
        PowerDown,
        Strike,
        Pause
    }

    public KeyCode powerUp, powerDown, strike, pause;

    public Keybindings() { }

    public Keybindings(KeyCode powerUp, KeyCode powerDown, KeyCode strike, KeyCode pause)
    {
        this.powerUp = powerUp;
        this.powerDown = powerDown;
        this.strike = strike;
        this.pause = pause;
    }

    public KeyCode CheckKey(ControlKey key)
    {
        switch(key)
        {
            case ControlKey.PowerUp:
                return powerUp;
            case ControlKey.PowerDown:
                return powerDown;
            case ControlKey.Strike:
                return strike;
            case ControlKey.Pause:
                return pause;

            default:
                return KeyCode.None;
        }
    }

    public void ChangeKey(ControlKey key, KeyCode code)
    {
        switch (key)
        {
            case ControlKey.PowerUp:
                powerUp = code;
                break;
            case ControlKey.PowerDown:
                powerDown = code;
                break;
            case ControlKey.Strike:
                strike = code;
                break;
            case ControlKey.Pause:
                pause = code;
                break;

            default:
                break;
        }
    }

    public void LoadFromSave(KeybindingsSave save)
    {
        powerUp = save.powerUp;
        powerDown = save.powerDown;
        strike = save.strike;
        pause = save.pause;
    }
}
