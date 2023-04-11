using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Dropdown resoDrop;
    public Slider volumeSlider;
    public bool inGame;

    void Start()
    {
        if (PlayerPrefs.GetInt("settingsSaved", 0) == 0)
        {
            PlayerPrefs.SetInt("resolution", 0);
            PlayerPrefs.SetFloat("mastervolume", 1.0f);
        }
        //Resolution
        if (PlayerPrefs.GetInt("resolution", 2) == 2)
        {
            resoDrop.value = 0;
            Screen.SetResolution(854, 480, true);
        }
        if (PlayerPrefs.GetInt("resolution", 1) == 1)
        {
            resoDrop.value = 1;
            Screen.SetResolution(1280, 720, true);
        }
        if (PlayerPrefs.GetInt("resolution", 0) == 0)
        {
            resoDrop.value = 2;
            Screen.SetResolution(1920, 1080, true);
        }
        //Volume
        volumeSlider.value = PlayerPrefs.GetFloat("mastervolume");
        AudioListener.volume = PlayerPrefs.GetFloat("mastervolume");
    }
    public void setResolution()
    {
        if (resoDrop.value == 0)
        {
            PlayerPrefs.SetInt("resolution", 2);
            PlayerPrefs.Save();
            Screen.SetResolution(854, 480, true);
        }
        if (resoDrop.value == 1)
        {
            PlayerPrefs.SetInt("resolution", 1);
            PlayerPrefs.Save();
            Screen.SetResolution(1280, 720, true);
        }
        if (resoDrop.value == 2)
        {
            PlayerPrefs.SetInt("resolution", 0);
            PlayerPrefs.Save();
            Screen.SetResolution(1920, 1080, true);
        }
    }
    public void setVolume()
    {
        PlayerPrefs.SetFloat("mastervolume", volumeSlider.value);
        PlayerPrefs.Save();
        AudioListener.volume = volumeSlider.value;
    }
    public void saveSettings()
    {
        PlayerPrefs.SetInt("settingsSaved", 1);
        PlayerPrefs.Save();
    }
}
