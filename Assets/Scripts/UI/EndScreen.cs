using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EndScreen : MonoBehaviour
{
    public GameObject endScreen;
    public GameObject gameView;
    public TMP_Text mainText;
    public TMP_Text secondaryText;
    public TMP_Text positionText;
    public TMP_InputField nameInput;
    public Animator transition;
    public float transitionTime = 1f;

    private Highscores highscores;

    private void OnEnable()
    {
        HoleTrigger.trigger += ActivateEndScreen;
    }

    private void OnDisable()
    {
        HoleTrigger.trigger -= ActivateEndScreen;
    }

    public void ActivateEndScreen()
    {
        Timer.Stop();
        MapSettings.isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PauseMenu.AbilityToPause(false);
        gameView.SetActive(false);
        endScreen.SetActive(true);

        PlayerData playerData = GameObject.FindGameObjectsWithTag("PlayerMarker")[0].GetComponent<PlayerData>();

        string jsonLoad = PlayerPrefs.GetString("highscores");
        highscores = JsonUtility.FromJson<Highscores>(jsonLoad);
        if (highscores == null) highscores = new Highscores();

        highscores.highscoreEntries = highscores.highscoreEntries.FindAll(e => e.mapSize == MapSettings.mapSize);
        highscores.highscoreEntries = highscores.highscoreEntries.FindAll(e => e.gameMode == MapSettings.gameMode);
        

        int position = 1;

        switch (MapSettings.gameMode)
        {
            case MapSettings.GameMode.Classic:
                mainText.text = "Strikes: " + playerData.strikes.ToString();
                secondaryText.text = "Time: " + GetTimeString();
                highscores.SortHighscoresByStrikes();
                for (int i = 0; i!= highscores.highscoreEntries.Count; i++)
                {
                    if (highscores.highscoreEntries[i].strikes > playerData.strikes || (highscores.highscoreEntries[i].strikes == playerData.strikes && highscores.highscoreEntries[i].time > Timer.time)) break;
                    else position++;
                }
                positionText.text = GetPositionString(position);
                break;
            case MapSettings.GameMode.TimeRush:
                mainText.text = "Time: " + GetTimeString();
                secondaryText.text = "Strikes: " + playerData.strikes.ToString();
                highscores.SortHighscoresByTime();
                for (int i = 0; i != highscores.highscoreEntries.Count; i++)
                {
                    if (highscores.highscoreEntries[i].time > Timer.time || (highscores.highscoreEntries[i].time == Timer.time && highscores.highscoreEntries[i].strikes > playerData.strikes)) break;
                    else position++;
                }
                positionText.text = GetPositionString(position);
                break;
        }
    }

    public void BackToMainMenu()
    {
        endScreen.SetActive(false);
        MapSettings.isGamePaused = false;
        StartCoroutine(LoadMainMenuWithTransition());
    }

    IEnumerator LoadMainMenuWithTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        PauseMenu.AbilityToPause(true);
        SceneManager.LoadScene(0);
    }
    public void QuitAndSave()
    {
        PlayerData playerData = GameObject.FindGameObjectsWithTag("PlayerMarker")[0].GetComponent<PlayerData>();

        string jsonLoad = PlayerPrefs.GetString("highscores");
        highscores = JsonUtility.FromJson<Highscores>(jsonLoad);
        if (highscores == null) highscores = new Highscores();

        highscores.highscoreEntries.Add(new HighscoreEntry(nameInput.text, MapSettings.seed, playerData.strikes, Timer.time, MapSettings.gameMode, MapSettings.mapSize));

        string jsonSave = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscores", jsonSave);
        PlayerPrefs.Save();

        BackToMainMenu();
    }

    private string GetTimeString()
    {
        string timeString = "";
        float seconds = Mathf.FloorToInt(Timer.time % 60);
        float minutes = Mathf.FloorToInt(Timer.time / 60);
        float hours = Mathf.FloorToInt(Timer.time / 3600);

        if (Timer.time >= 3600) timeString += hours + "h ";
        if (Timer.time >= 60) timeString += minutes + "min ";
        timeString += seconds + "s";

        return timeString;
    }

    private string GetPositionString(int position)
    {
        string positionString = "";

        switch (position)
        {
            case 1:
                positionString = "1ST";
                break;
            case 2:
                positionString = "2ND";
                break;
            case 3:
                positionString = "3RD";
                break;
            default:
                positionString = position + "TH";
                break;
        }

        return positionString;
    }
}
