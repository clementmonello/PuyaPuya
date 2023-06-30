using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject PauseScreen;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void CallPauseScreen() 
    {
        PauseScreen.SetActive(true);
        Time.timeScale = 0;
    }

    public void ReturnToMenu() 
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void Resume() 
    {
        Time.timeScale = 1;
    }
}
