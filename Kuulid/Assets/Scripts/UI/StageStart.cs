using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageStart : MonoBehaviour
{
    [SerializeField] float startCounter;
    [SerializeField] TMP_Text startCounterText;
    private WaveController wController;

    private void Awake()
    {
        wController = FindAnyObjectByType<WaveController>();
    }

    private void Update()
    {
        if (!wController.hasStarted)
        {
            if (startCounter > 0)
            {
                startCounter -= Time.deltaTime;
                UpdateCounter(startCounter);
            }
            else
            {
                startCounter = 0;
                wController.hasStarted = true;
                startCounterText.gameObject.SetActive(false);
            }
        }
    }

    private void UpdateCounter(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        startCounterText.text = seconds.ToString();
    }
}
