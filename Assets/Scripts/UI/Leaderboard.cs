using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Leaderboard : MonoBehaviour
{
    public TMP_Dropdown gameModeDropdown = null;
    public TMP_Dropdown sizeDropdown = null;
    public TMP_InputField seedInput = null;
    public GameObject strikesLabel = null;
    public GameObject timeLabel = null;
    
    private Transform highscoreEntryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        highscoreEntryTemplate = transform.Find("HighscoreEntryTemplate");
        highscoreEntryTemplate.gameObject.SetActive(false);

        gameModeDropdown.ClearOptions();
        gameModeDropdown.AddOptions(MapSettings.gameModeNameList);
        sizeDropdown.ClearOptions();
        sizeDropdown.AddOptions(MapSettings.mapSizeNameList);

        FillLeaderboard();
    }

    private void CreateHighscoreEntryTemplate(HighscoreEntry highscoreEntry, List<Transform> transforms)
    {
        Transform entry = Instantiate(highscoreEntryTemplate, transform);

        string position = (transforms.Count + 1).ToString();
        TMP_Text positionText = entry.Find("Position").GetComponent<TMP_Text>();
        TMP_Text nameText = entry.Find("Name").GetComponent<TMP_Text>();
        TMP_Text seedText = entry.Find("Seed").GetComponent<TMP_Text>();
        TMP_Text scoreText = entry.Find("Score").GetComponent<TMP_Text>();
        switch (position)
        {
            case "1":
                position = "1ST";
                positionText.color = Color.green;
                nameText.color = Color.green;
                seedText.color = Color.green;
                scoreText.color = Color.green;
                break;
            case "2":
                position = "2ND";
                positionText.color = Color.green;
                nameText.color = Color.green;
                seedText.color = Color.green;
                scoreText.color = Color.green;
                break;
            case "3":
                position = "3RD";
                positionText.color = Color.green;
                nameText.color = Color.green;
                seedText.color = Color.green;
                scoreText.color = Color.green;
                break;
            default:
                position = position + "TH";
                break;
        }

        positionText.text = position;
        nameText.text = highscoreEntry.name;
        seedText.text = highscoreEntry.seed;

        int gameMode = gameModeDropdown.value;
        switch (gameMode)
        {
            case 0:
                scoreText.text = highscoreEntry.strikes.ToString();
                break;
            case 1:
                scoreText.text = "";
                float seconds = Mathf.FloorToInt(highscoreEntry.time % 60);
                float minutes = Mathf.FloorToInt(highscoreEntry.time / 60);
                float hours = Mathf.FloorToInt(highscoreEntry.time / 3600);

                if (highscoreEntry.time >= 3600) scoreText.text += hours + "h ";
                if (highscoreEntry.time >= 60) scoreText.text += minutes + "min ";
                scoreText.text += seconds + "s";
                break;
        }
        entry.gameObject.SetActive(true);

        transforms.Add(entry);
    }

    public void FillLeaderboard()
    {
        foreach (Transform child in transform)
        {
            if (child != highscoreEntryTemplate) Destroy(child.gameObject);
        }

        string jsonLoad = PlayerPrefs.GetString("highscores");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonLoad);
        if (highscores == null) highscores = new Highscores();

        int size = sizeDropdown.value;
        highscores.highscoreEntries = highscores.highscoreEntries.FindAll(e => e.mapSize == (MapSettings.MapSize)size);

        int gameMode = gameModeDropdown.value;
        highscores.highscoreEntries = highscores.highscoreEntries.FindAll(e => e.gameMode == (MapSettings.GameMode)gameMode);
        switch (gameMode)
        {
            case 0:
                highscores.SortHighscoresByStrikes();
                strikesLabel.SetActive(true);
                timeLabel.SetActive(false);
                break;
            case 1:
                highscores.SortHighscoresByTime();
                strikesLabel.SetActive(false);
                timeLabel.SetActive(true);
                break;
        }

        string seed = seedInput.text;
        if (seed != "") highscores.highscoreEntries = highscores.highscoreEntries.FindAll(e => e.seed == seed);

        highscoreEntryTransformList = new List<Transform>();

        foreach (HighscoreEntry entry in highscores.highscoreEntries)
        {
            CreateHighscoreEntryTemplate(entry, highscoreEntryTransformList);
        }
    }
}
