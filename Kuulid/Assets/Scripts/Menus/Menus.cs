using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("StageSelection");
        Time.timeScale = 1.0f;
    }

    public void LoadStage1()
    {
        SceneManager.LoadScene("Stage_1");
    }

    public void LoadStage2()
    {
        SceneManager.LoadScene("Stage_2");
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1.0f;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
