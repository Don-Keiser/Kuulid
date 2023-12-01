using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VictoryController : MonoBehaviour
{
    [SerializeField] GameObject WinMenu;
    [SerializeField] TMP_Text winLoseText;

    private WaveController wController;

    private string Won = "CONGRATULATIONS!\nYOU'VE WON!";
    private string Lost = "GAME OVER\n=(";

    private void Awake()
    {
        wController = FindAnyObjectByType<WaveController>();
    }

    private void Update()
    {
        if (wController.bossDefeated)
        {
            WinMenu.SetActive(true);
            winLoseText.text = Won;
            Time.timeScale = 0.0f;
        }
        if (PlayerStats.instance.isDead)
        {
            WinMenu.SetActive(true);
            winLoseText.text = Lost;
            Time.timeScale = 0.0f;
        }
    }
}
