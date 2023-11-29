using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;

    private Quaternion initialRotation;

    private void OnEnable()
    {
        initialRotation = transform.rotation;
    }

    private void FixedUpdate()
    {
        rb.velocity = initialRotation * Vector3.down * EnemiesStats.instance.eBulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            PlayerStats.instance.TakeDamage(EnemiesStats.instance.eDamage);
        }
    }
}
