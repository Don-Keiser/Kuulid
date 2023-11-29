using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform[] bulletPositionPattern1;
    [SerializeField] Transform bPP2G;
    [SerializeField] Transform[] bulletPositionPattern2;
    [SerializeField] Transform bulletPositionPattern3;
    [SerializeField] float changeCooldown;

    private float moveCooldown;
    private float changeShootCooldown = 5f;

    private bool isChoosingBetweenShoots = false;

    [SerializeField] float maxHeight;
    private float currentHeight;

    private bool circleTurning = false;

    private bool spiralTurning = false;

    private void Start()
    {
        StartCoroutine(ShootSpiral());
    }

    private void Update()
    {
        currentHeight = transform.position.y;

        moveCooldown += Time.deltaTime;
        changeShootCooldown -= Time.deltaTime;

        StartCoroutine(MovePaternHorizontal());

        if (!isChoosingBetweenShoots && changeShootCooldown <= 0)
        {
            ChooseBetweenShoots();
        }
    }

    private void FixedUpdate()
    {
        CheckPosition();
    }

    private void CheckPosition()
    {
        if (currentHeight > maxHeight)
        {
            transform.position = Vector2.Lerp(transform.position, new Vector2(transform.position.x, maxHeight), Time.deltaTime * 5);
        }
        else
        {
            return;
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
                GameObject bullet = EnemyObjectPool.instance.GetPooledObject();

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
        Debug.Log("Shoot2 is running");

        while (changeShootCooldown > 0)
        {
            yield return new WaitForSeconds(0.2f);

            if (!circleTurning)
            {
                bPP2G.rotation = bPP2G.rotation * Quaternion.Euler(0, 0, 20);

                if (bPP2G.rotation == Quaternion.Euler(0, 0, 20))
                {
                    circleTurning = true;
                }
            }
            else if (circleTurning)
            {
                bPP2G.rotation = bPP2G.rotation * Quaternion.Euler(0, 0, -20);

                if (bPP2G.rotation == Quaternion.Euler(0, 0, -20))
                {
                    circleTurning = false;
                }
            }

            foreach (Transform pos2 in bulletPositionPattern2)
            {
                GameObject bullet = EnemyObjectPool.instance.GetPooledObject();

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

            GameObject bullet = EnemyObjectPool.instance.GetPooledObject();

            if (bullet != null)
            {
                bullet.transform.position = bulletPositionPattern3.position;
                bullet.transform.localRotation = bulletPositionPattern3.rotation;
                bullet.SetActive(true);

                if (!spiralTurning)
                {
                    bulletPositionPattern3.rotation = bulletPositionPattern3.rotation * Quaternion.Euler(0, 0, 5);

                    if (bulletPositionPattern3.rotation == Quaternion.Euler(0, 0, 70))
                    {
                        spiralTurning = true;
                    }
                }
                else if (spiralTurning)
                {
                    bulletPositionPattern3.rotation = bulletPositionPattern3.rotation * Quaternion.Euler(0, 0, -5);

                    if (bulletPositionPattern3.rotation == Quaternion.Euler(0, 0, -70))
                    {
                        spiralTurning = false;
                    }
                }
            }
        }
    }

    private IEnumerator MoveCircularArc()
    {
        Debug.Log("isRun");

        yield return null;

        moveCooldown = 0;

        Vector2 startingPos = transform.position;
        Vector2 endPosRight = new Vector2(8f, transform.position.y);
        Vector2 controlpointRight = startingPos + (endPosRight - startingPos) / 2 + Vector2.down * 5.0f;
        Vector2 endPosLeft = new Vector2(8f, transform.position.y);
        Vector2 controlpointLeft = startingPos + (endPosLeft - startingPos) / 2 + Vector2.down * 5.0f;

        if (transform.position.y >= -4)
        {
            Vector2 m1 = Vector2.Lerp(startingPos, controlpointRight, Time.deltaTime);
            Vector2 m2 = Vector2.Lerp(startingPos, endPosRight, Time.deltaTime);
            transform.position = Vector2.Lerp(m1, m2, Time.deltaTime * 5f);
            Debug.Log("RightArc");
        }
        else if (transform.position.y <= 4)
        {
            Vector2 m1 = Vector2.Lerp(startingPos, controlpointLeft, Time.deltaTime);
            Vector2 m2 = Vector2.Lerp(startingPos, endPosLeft, Time.deltaTime);
            transform.position = Vector2.Lerp(m1, m2, Time.deltaTime * 5f);
            Debug.Log("LeftArc");
        }
        else
        {
            Vector2 middlePos = new Vector2(0, transform.position.y);
            transform.position = Vector2.Lerp(startingPos, middlePos, Time.deltaTime * 5f);
            Debug.Log("Other");
        }
    }

    private IEnumerator MovePaternHorizontal()
    {
        while (moveCooldown <= 5)
        {
            yield return null;
        }
        moveCooldown = 0;

        Vector2 MoveLeft = Vector2.Lerp(transform.position, new Vector2(-4f, transform.position.y), 1);
        Vector2 MoveRight = Vector2.Lerp(transform.position, new Vector2(4f, transform.position.y), 1);
        Vector2 MoveBackToOrigin = Vector2.Lerp(transform.position, new Vector2(0f, transform.position.y), 1);

        Vector2[] randomMove = new Vector2[] { MoveLeft, MoveRight };

        if (transform.position.x <= -4)
        {
            transform.position = MoveBackToOrigin;
        }
        else if (transform.position.x >= 4)
        {
            transform.position = MoveBackToOrigin;
        }
        else
        {
            transform.position = randomMove[Random.Range(0, 2)];
        }
    }
}
