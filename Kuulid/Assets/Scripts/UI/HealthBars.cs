using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    [SerializeField] Image pHealthBarForegroundImage;
    [SerializeField] Image eHealthBarForegroundImage;
    [SerializeField] GameObject eHealthBarHolder;

    private WaveController wController;

    private void Awake()
    {
        wController = FindAnyObjectByType<WaveController>();
    }

    private void Update()
    {
        if (wController.bossSpawned)
        {
            eHealthBarHolder.SetActive(true);
        }
    }

    public void UpdatePlayerHealthBar(PlayerStats health)
    {
        pHealthBarForegroundImage.fillAmount = health.pRemainingHealthPercentage;
    }

    public void UpdateBossHealthBar(EnemiesStats health)
    {
        eHealthBarForegroundImage.fillAmount = health.eRemainingHealthPercentage;
    }
}
