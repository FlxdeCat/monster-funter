using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    public static bool isPaused = false;
    [SerializeField] private GameObject pausePanel;

    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "MainMenu")
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        CharacterMovement.setstopMove(false);
        CharacterMovement.setStopSkills(false);
        if (!WizardFlySkill.isFlying()) CharacterMovement.disableGravity(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        CharacterMovement.setstopMove(true);
        CharacterMovement.setStopSkills(true);
        if (!WizardFlySkill.isFlying()) CharacterMovement.disableGravity(true);
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Exit()
    {
        CharacterMovement.setstopMove(false);
        CharacterMovement.setStopSkills(false);
        CharacterMovement.disableGravity(false);
        Time.timeScale = 1f;
        LoadingScreenManager.loadingScreenInstance.LoadNextScene("MainMenu");
    }

}
