using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MapSettingsMenu : MonoBehaviour
{
    public TMP_Dropdown themeDropdown;
    public TMP_Dropdown sizeDropdown;
    public TMP_InputField seedInput;
    public SeedSelect seedSelect;
    public Animator transition;
    public float transitionTime = 1f;

    private void Start()
    {
        themeDropdown.ClearOptions();
        sizeDropdown.ClearOptions();

        themeDropdown.AddOptions(MapSettings.themeNameList);
        sizeDropdown.AddOptions(MapSettings.mapSizeNameList);

        themeDropdown.value = (int)MapSettings.theme;
        sizeDropdown.value = (int)MapSettings.mapSize;
        themeDropdown.RefreshShownValue();
        sizeDropdown.RefreshShownValue();
    }

    public void ChangeTheme(int id)
    {
        MapSettings.theme = (MapSettings.Theme)id;
    }

    public void ChangeMapSize(int id)
    {
        MapSettings.mapSize = (MapSettings.MapSize)id;
    }

    public void PlayGame()
    {
        if (seedInput.text == "") seedSelect.useRandomSeed = true;
        else
        {
            seedSelect.useRandomSeed = false;
            seedSelect.seedString = seedInput.text;
            MapSettings.seed = seedInput.text;
        }
        seedSelect.UpdateSeed();
        if (seedInput.text == "") MapSettings.seed = seedSelect.seed.ToString();

        StartCoroutine(LoadLevelWithTransition(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevelWithTransition(int levelIndex)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
