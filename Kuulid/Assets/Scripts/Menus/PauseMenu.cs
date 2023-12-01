using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject pauseManager;

    private bool isPaused = false;

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pauseMenu.SetActive(true);
            pauseManager.SetActive(false);
            Time.timeScale = 0.0f;
        }
    }

    public void Resume(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pauseMenu.SetActive(false);
            pauseManager.SetActive(true);
            Time.timeScale = 1.0f;
        }
    }
}
