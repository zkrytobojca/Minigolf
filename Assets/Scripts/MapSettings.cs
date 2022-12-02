using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSettings
{
    public enum GameMode
    {
        Classic,
        TimeRush
    }

    public enum Theme
    {
        GreenHills
    }

    public enum MapSize
    {
        Small,
        Medium,
        Large
    }

    public static GameMode gameMode = GameMode.Classic;
    public static Theme theme = Theme.GreenHills;
    public static MapSize mapSize = MapSize.Small;
    public static bool isGamePaused = false;
    public static string seed;

    public static List<string> gameModeNameList = new List<string>() { "Classic", "TimeRush" };
    public static List<string> themeNameList = new List<string>() { "Green Hills" };
    public static List<string> mapSizeNameList = new List<string>() { "Small", "Medium", "Large" };
}
