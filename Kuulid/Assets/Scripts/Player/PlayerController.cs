using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 movement;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPosition;
    [SerializeField] float cooldown;
    private float cooldownTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DamageTest();
        }

        Shoot();
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private void Shoot()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer > 0) return;

        cooldownTimer = cooldown;

        GameObject bullet = ObjectPool.instance.GetPooledObject();

        if (bullet != null)
        {
            bullet.transform.position = bulletPosition.position;
            bullet.SetActive(true);
        }
    }

    public void DamageTest()
    {
        PlayerStats.instance.TakeDamage(1);
    }
}
