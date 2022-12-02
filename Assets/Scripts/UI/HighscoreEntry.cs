using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighscoreEntry
{
    public string name;
    public string seed;
    public int strikes;
    public float time;

    public MapSettings.GameMode gameMode;
    public MapSettings.MapSize mapSize;

    public HighscoreEntry(string name, string seed, int strikes, float time, MapSettings.GameMode gameMode, MapSettings.MapSize mapSize)
    {
        this.name = name;
        this.seed = seed;
        this.strikes = strikes;
        this.time = time;
        this.gameMode = gameMode;
        this.mapSize = mapSize;
    }
}
