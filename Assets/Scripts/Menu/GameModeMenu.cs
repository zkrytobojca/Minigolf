using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeMenu : MonoBehaviour
{
    public void SelectClassicMode()
    {
        MapSettings.gameMode = MapSettings.GameMode.Classic;
    }

    public void SelectTimeRushMode()
    {
        MapSettings.gameMode = MapSettings.GameMode.TimeRush;
    }
}
