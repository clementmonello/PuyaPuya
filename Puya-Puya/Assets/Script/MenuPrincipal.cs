using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public GameObject menuObj, settingsObj;
    public string sceneName="Gameplay";

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
    }
    public void playGame()
    {
        PlayerPrefs.SetInt("level", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
    public void SettingsMenu()
    {
        menuObj.SetActive(false);
        settingsObj.SetActive(true);
    }
    public void quitGame()
    {
        Application.Quit();
    }
    public void backToMenu()
    {
        settingsObj.SetActive(false);
        menuObj.SetActive(true);
    }
}
