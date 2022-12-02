using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public TMP_Text seedDisplay;
    public Animator transition;
    public float transitionTime = 1f;

    private static bool canPause = true;

    private void Start()
    {
        seedDisplay.text = "Seed: " + MapSettings.seed;
    }

    private void Update()
    {
        if (InputManager.instance.KeyDown(Keybindings.ControlKey.Pause) && canPause == true)
        {
            if (MapSettings.isGamePaused == false)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        MapSettings.isGamePaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
        MapSettings.isGamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void BackToMainMenu()
    {
        AbilityToPause(false);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
        MapSettings.isGamePaused = false;
        StartCoroutine(LoadMainMenuWithTransition());
    }

    IEnumerator LoadMainMenuWithTransition()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        AbilityToPause(true);
        SceneManager.LoadScene(0);
    }

    public static void AbilityToPause(bool ability)
    {
        canPause = ability;
    }
}
