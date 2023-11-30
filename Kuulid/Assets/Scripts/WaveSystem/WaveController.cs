using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveController : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform[] spawnPos;
    [SerializeField] Transform bossPos;
    [Header("GameObjects")]
    [SerializeField] GameObject simpleEnemy;
    [SerializeField] GameObject Boss;
    [Header("Integers")]
    public int nWaves;
    public int eCounter;
    [Header("Bools")]
    public bool hasStarted = false;
    public bool bossDefeated = false;
    public bool bossSpawned = false;

    //public float
    public float waveEnemyDefeated = 0f;

    //private bools
    private bool waveStarted = false;


    private void Update()
    {
        if (!waveStarted && hasStarted)
        {
            if (nWaves > 0f)
            {
                StartCoroutine(SpawnEnemies());
            }
            else if (nWaves <= 0f && !bossSpawned) 
            {
                StartCoroutine(SpawnBoss());
                bossSpawned = true;
            }

            //CheckNWaves();
        }

        CheckEnemiesCount();
    }

    private void CheckEnemiesCount()
    {
        if (waveEnemyDefeated >= 3)
        {
            waveEnemyDefeated = 0;
            nWaves--;
            waveStarted = false;
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (waveStarted)
        {
            yield return null;
        }

        foreach (Transform pos in spawnPos) 
        { 
            GameObject spawnedEnemy = Instantiate(simpleEnemy);

            spawnedEnemy.transform.position = pos.transform.position;
        }
        waveStarted = true;
    }

    private IEnumerator SpawnBoss()
    {
        while (waveStarted)
        {
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        Boss.SetActive(true);

        waveStarted = true;
    }
}
