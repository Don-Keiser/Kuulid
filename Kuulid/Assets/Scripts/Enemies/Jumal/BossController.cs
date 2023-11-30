using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject bulletPrefab;
    [Header("Transforms")]
    [SerializeField] Transform[] bulletPositionPattern1;
    [SerializeField] Transform bPP2G;
    [SerializeField] Transform[] bulletPositionPattern2;
    [SerializeField] Transform bulletPositionPattern3;
    [Header("Floats")]
    [SerializeField] float changeCooldown;

    //private floats
    private float moveCooldown = 5f;
    private float changeShootCooldown = 5f;

    //private bools
    private bool isChoosingBetweenShoots = false;
    private bool circleTurning = false;
    private bool spiralTurning = false;

    private void Start()
    {
        //Starts the shoot Spiral Coroutine so Boss shoots immediately when instantiated
        StartCoroutine(ShootSpiral());
    }

    private void Update()
    {

        moveCooldown -= Time.deltaTime;
        changeShootCooldown -= Time.deltaTime;

        StartCoroutine(MovePaternHorizontal());

        if (!isChoosingBetweenShoots && changeShootCooldown <= 0)
        {
            ChooseBetweenShoots();
        }
    }

    private void ChooseBetweenShoots()
    {
        StopAllCoroutines();

        isChoosingBetweenShoots = true;

        changeShootCooldown = changeCooldown;

        var rand = Random.Range(0, 3);

        if (rand == 0)
        {
            StartCoroutine(Shoot());
        }

        else if (rand == 1)
        {
            StartCoroutine(ShootCircle());
        }

        else if (rand == 2)
        {
            StartCoroutine(ShootSpiral());
        }

        StartCoroutine(ResetChoosingShoot());
    }

    private IEnumerator ResetChoosingShoot()
    {
        yield return new WaitForSeconds(0.5f);
        isChoosingBetweenShoots = false;
    }

    private IEnumerator Shoot()
    {
        Debug.Log("Shoot1 is running");

        while (changeShootCooldown > 0)
        {
            yield return new WaitForSeconds(0.2f);

            foreach (Transform pos in bulletPositionPattern1)
            {
                GameObject bullet = BossObjectPool.instance.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = pos.position;
                    bullet.transform.localRotation = pos.rotation;
                    bullet.SetActive(true);
                }
            }
        }
    }

    private IEnumerator ShootCircle()
    {

        while (changeShootCooldown > 0)
        {
            yield return new WaitForSeconds(0.2f);

            if (!circleTurning)
            {
                bPP2G.rotation = bPP2G.rotation * Quaternion.Euler(0, 0, 10);

                if (bPP2G.rotation == Quaternion.Euler(0, 0, 20))
                {
                    circleTurning = true;
                }
            }
            else if (circleTurning)
            {
                bPP2G.rotation = bPP2G.rotation * Quaternion.Euler(0, 0, -10);

                if (bPP2G.rotation == Quaternion.Euler(0, 0, -20))
                {
                    circleTurning = false;
                }
            }

            foreach (Transform pos2 in bulletPositionPattern2)
            {
                GameObject bullet = BossObjectPool.instance.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = pos2.position;
                    bullet.transform.localRotation = pos2.rotation;
                    bullet.SetActive(true);
                }
            }
        }
    }

    private IEnumerator ShootSpiral()
    {
        while (changeShootCooldown > 0)
        {
            yield return new WaitForSeconds(0.01f);

            GameObject bullet = BossObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = bulletPositionPattern3.position;
                bullet.transform.localRotation = bulletPositionPattern3.rotation;
                bullet.SetActive(true);

                if (!spiralTurning)
                {
                    bulletPositionPattern3.rotation = bulletPositionPattern3.rotation * Quaternion.Euler(0, 0, 7);

                    if (bulletPositionPattern3.rotation == Quaternion.Euler(0, 0, 70))
                    {
                        spiralTurning = true;
                    }
                }
                else if (spiralTurning)
                {
                    bulletPositionPattern3.rotation = bulletPositionPattern3.rotation * Quaternion.Euler(0, 0, -7);

                    if (bulletPositionPattern3.rotation == Quaternion.Euler(0, 0, -70))
                    {
                        spiralTurning = false;
                    }
                }
            }
        }
    }

    private IEnumerator MovePaternHorizontal()
    {
        //while moveCooldown hasn't reached past 0, do not do the coroutine
        while (moveCooldown > 0)
        {
            yield return null;
        }

        Vector2 MoveLeft = Vector2.Lerp(transform.position, new Vector2(-4f, transform.position.y), 1);
        Vector2 MoveRight = Vector2.Lerp(transform.position, new Vector2(4f, transform.position.y), 1);
        Vector2 MoveBackToOrigin = Vector2.Lerp(transform.position, new Vector2(0f, transform.position.y), 1);

        Vector2[] randomMove = new Vector2[] { MoveLeft, MoveRight };

        if (transform.position.x <= -3 || transform.position.x >= 3)
        {
            transform.position = MoveBackToOrigin;
        }
        else
        {
            transform.position = randomMove[Random.Range(0, 2)];
        }
        moveCooldown = 5f;
    }
}
