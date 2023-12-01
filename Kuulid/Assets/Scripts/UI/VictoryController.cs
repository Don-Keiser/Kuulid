using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour
{
    private WaveController wController;
    [SerializeField] GameObject WinMenu;
    [SerializeField] float timeShow;

    private void Awake()
    {
        wController = FindAnyObjectByType<WaveController>();
    }

    private void Update()
    {
        timeShow = Time.timeScale;

        if (wController.bossDefeated)
        {
            WinMenu.SetActive(true);
            Time.timeScale = 0.0f;
        }
    }
}
