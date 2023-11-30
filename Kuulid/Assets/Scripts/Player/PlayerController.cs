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
    private WaveController wController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wController = FindAnyObjectByType<WaveController>();
    }

    private void Update()
    {
        cooldownTimer -= Time.deltaTime;

        //If cooldown inferior or equal to 0, strat shoot coroutine
        if (cooldownTimer <= 0 && wController.hasStarted)
            StartCoroutine("Shoot");
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * speed;
    }

    private void OnMovement(InputValue value)
    {
        movement = value.Get<Vector2>();
    }

    private IEnumerator Shoot()
    {

        yield return null;

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
