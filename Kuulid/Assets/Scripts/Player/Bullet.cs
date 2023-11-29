using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] Rigidbody2D rb;

    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * PlayerStats.instance.pBulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Edge"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Enemies"))
        {
            gameObject.SetActive(false);
            collision.gameObject.GetComponent<EnemiesStats>().TakeDamage(PlayerStats.instance.pDamage);
        }
    }
}
