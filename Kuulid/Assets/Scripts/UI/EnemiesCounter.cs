using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemiesCounter : MonoBehaviour
{
    [SerializeField] TMP_Text eCounterText;
    private WaveController wController;

    private int eTotal;

    private void Awake()
    {
        wController = FindAnyObjectByType<WaveController>();

        eTotal = wController.nWaves * 3;
        wController.eCounter = eTotal;
    }

    private void Update()
    {
        if (wController.eCounter > 0)
        {
            EnemyCounterUpdate();
        }
        else
        {
            wController.eCounter = 0;
            eCounterText.gameObject.SetActive(false);
        }
    }

    private void EnemyCounterUpdate()
    {
        eCounterText.text = "Enemies Left:\n" + wController.eCounter + "/" + eTotal;
    }
}
