using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionMenuScript : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void setMusic (float musicValue)
    {
        audioMixer.SetFloat("MusicVolume", musicValue);
    }

    public void setAudio(float audioValue)
    {
        audioMixer.SetFloat("AudioVolume", audioValue);
    }

    public void setGraphics (int graphicIndex)
    {
        QualitySettings.SetQualityLevel(graphicIndex);
    }

    public void setFullscreen (bool setFullscreen)
    {
        Screen.fullScreen = setFullscreen;
    }

}
