using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class MainMenuScript : MonoBehaviour
{

    [SerializeField] private CinemachineVirtualCamera cam1;
    [SerializeField] private CinemachineVirtualCamera cam2;

    public void Start()
    {
        cam1.Priority = 1;
        cam2.Priority = 0;
    }
    public void PlayGame()
    {
        cam1.Priority = 0;
        cam2.Priority = 1;
    }
    public void BackMenu()
    {
        cam1.Priority = 1;
        cam2.Priority = 0;
    }
    public void QuitGame()
    {
        //Debug.Log("Quitting game...");
        Application.Quit();
    }

}
