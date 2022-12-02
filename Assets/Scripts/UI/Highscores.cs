using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highscores
{
    public List<HighscoreEntry> highscoreEntries;

    public Highscores()
    {
        highscoreEntries = new List<HighscoreEntry>();
    }

    public Highscores(List<HighscoreEntry> entries)
    {
        highscoreEntries = entries;
    }

    public void SortHighscoresByStrikes()
    {
        for (int i = 0; i != highscoreEntries.Count; i++)
        {
            for (int j = i + 1; j != highscoreEntries.Count; j++)
            {
                if (highscoreEntries[i].strikes > highscoreEntries[j].strikes || (highscoreEntries[i].strikes == highscoreEntries[j].strikes && highscoreEntries[i].time > highscoreEntries[j].time))
                {
                    HighscoreEntry swap = highscoreEntries[i];
                    highscoreEntries[i] = highscoreEntries[j];
                    highscoreEntries[j] = swap;
                }
            }
        }
    }

    public void SortHighscoresByTime()
    {
        for (int i = 0; i != highscoreEntries.Count; i++)
        {
            for (int j = i + 1; j != highscoreEntries.Count; j++)
            {
                if (highscoreEntries[i].time > highscoreEntries[j].time || (highscoreEntries[i].strikes == highscoreEntries[j].strikes && highscoreEntries[i].strikes > highscoreEntries[j].strikes))
                {
                    HighscoreEntry swap = highscoreEntries[i];
                    highscoreEntries[i] = highscoreEntries[j];
                    highscoreEntries[j] = swap;
                }
            }
        }
    }
}
