using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBars : MonoBehaviour
{
    [SerializeField] Image pHealthBarForegroundImage;
    [SerializeField] Image eHealthBarForegroundImage;

    public void UpdatePlayerHealthBar(PlayerStats health)
    {
        pHealthBarForegroundImage.fillAmount = health.pRemainingHealthPercentage;
    }

    public void UpdateBossHealthBar(EnemiesStats health)
    {
        eHealthBarForegroundImage.fillAmount = health.eRemainingHealthPercentage;
    }
}
