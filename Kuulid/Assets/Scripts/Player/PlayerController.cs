using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPosition;
    private WaveController wController;
    private Vector2 movement;

    private bool beganShoot = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        wController = FindAnyObjectByType<WaveController>();
    }

    private void Update()
    {
        if (!beganShoot)
        {
            StartCoroutine(Shoot());
        }
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
        while (wController.hasStarted)
        {
            beganShoot = true;

            yield return new WaitForSeconds(0.1f);

            GameObject bullet = ObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = bulletPosition.position;
                bullet.SetActive(true);
            }
        }
    }

    public void DamageTest()
    {
        PlayerStats.instance.TakeDamage(1);
    }
}
