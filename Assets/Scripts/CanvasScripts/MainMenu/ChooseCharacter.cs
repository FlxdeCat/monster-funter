using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChooseCharacter : MonoBehaviour
{

    Animator animator;
    AudioSource characterAudio;
    [SerializeField] private GameObject particle1;
    [SerializeField] private Light characterLight1;
    [SerializeField] private GameObject particle2;
    [SerializeField] private Light characterLight2;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterAudio = GetComponent<AudioSource>();
    }
    public void isPaladinHover()
    {
        PlayerPrefs.SetString("chosenCharacter", "Paladin");
        animator.SetBool("hover", true);
        characterLight1.intensity = 1;
        characterLight2.intensity = 0;
        characterAudio.mute = false;
        characterAudio.Play();
        particle1.SetActive(true);
        particle2.SetActive(false);
    }
    public void isWizardHover()
    {
        PlayerPrefs.SetString("chosenCharacter", "Wizard");
        animator.SetBool("hover", true);
        characterLight1.intensity = 0;
        characterLight2.intensity = 1;
        characterAudio.mute = false;
        characterAudio.Play();
        particle1.SetActive(false);
        particle2.SetActive(true);
    }
    public void notHover()
    {
        animator.SetBool("hover", false);
        characterAudio.mute = true;
    }
    public void startGame(string sceneName)
    {
        LoadingScreenManager.loadingScreenInstance.LoadNextScene(sceneName);
    }

}
