using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public Slider volumeSliderMaster;
    public Slider volumeSliderGame;
    public Slider volumeSliderUI;
    public Slider volumeSliderMusic;
    public TMP_Dropdown graphicsDropdown;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionId = 0;
        for (int i=0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionId = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        if (PlayerPrefs.HasKey("resolution")) resolutionDropdown.value = PlayerPrefs.GetInt("resolution");
        else resolutionDropdown.value = currentResolutionId;
        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = Screen.fullScreen;

        if (PlayerPrefs.HasKey("volume")) volumeSliderMaster.value = PlayerPrefs.GetFloat("volume");
        if (PlayerPrefs.HasKey("volume_game")) volumeSliderGame.value = PlayerPrefs.GetFloat("volume_game");
        if (PlayerPrefs.HasKey("volume_ui")) volumeSliderUI.value = PlayerPrefs.GetFloat("volume_ui");
        if (PlayerPrefs.HasKey("volume_music")) volumeSliderMusic.value = PlayerPrefs.GetFloat("volume_music");

        if (PlayerPrefs.HasKey("quality")) graphicsDropdown.value = PlayerPrefs.GetInt("quality");
        graphicsDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void SetResolution(int resolutionId)
    {
        Resolution resolution = resolutions[resolutionId];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        PlayerPrefs.SetInt("resolution", resolutionId);
        PlayerPrefs.Save();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
    }

    public void SetVolumeGame(float volume)
    {
        audioMixer.SetFloat("VolumeGame", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("volume_game", volume);
        PlayerPrefs.Save();
    }

    public void SetVolumeUI(float volume)
    {
        audioMixer.SetFloat("VolumeUI", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("volume_ui", volume);
        PlayerPrefs.Save();
    }

    public void SetVolumeMusic(float volume)
    {
        audioMixer.SetFloat("VolumeMusic", Mathf.Log10(volume) * 20);

        PlayerPrefs.SetFloat("volume_music", volume);
        PlayerPrefs.Save();
    }

    public void SetQuality(int qualityId)
    {
        QualitySettings.SetQualityLevel(qualityId);

        PlayerPrefs.SetInt("quality", qualityId);
        PlayerPrefs.Save();
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
