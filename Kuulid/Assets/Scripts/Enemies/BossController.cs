using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] bulletPosition;
    [SerializeField] float cooldown;
    private float cooldownTimer;

    private void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0) return;

        cooldownTimer = Random.Range(0.6f, 1.4f);

        foreach (Transform pos in bulletPosition)
        {
            GameObject bullet = EnemyObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {

                bullet.transform.position = pos.position;
                bullet.SetActive(true);
            }
        }
    }
}
