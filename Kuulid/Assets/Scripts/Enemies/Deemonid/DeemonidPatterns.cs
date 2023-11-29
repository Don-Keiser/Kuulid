using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeemonidPatterns : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform BPPG;
    [SerializeField] Transform[] bulletPositionPattern1;

    [SerializeField] float movementSpeed;

    private Vector2 movementDir;

    private bool rotateShoot = false;
    private bool isAtOrigin = true;

    private Vector3 originalPos;
    private Vector3 currentPos;

    private void Start()
    {
        originalPos = transform.position;
        StartCoroutine("Shoot");
        StartCoroutine(DiagonalMovements());
    }

    private void Update()
    {
        currentPos = transform.position;
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.15f);

            if (!rotateShoot)
            {
                BPPG.rotation = BPPG.rotation * Quaternion.Euler(0, 0, 5);

                if (BPPG.rotation == Quaternion.Euler(0, 0, 15))
                {
                    rotateShoot = true;
                }
            }
            else if (rotateShoot)
            {
                BPPG.rotation = BPPG.rotation * Quaternion.Euler(0, 0, -5);

                if (BPPG.rotation == Quaternion.Euler(0, 0, -15))
                {
                    rotateShoot = false;
                }
            }

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

    private void HorizontalMovements()
    {
        var leftDir = Vector2.left;
        var rightDir = Vector2.right;

        transform.Translate(movementDir * movementSpeed * Time.deltaTime);

        if (transform.position.x <= -9)
        { movementDir = rightDir; }
        else if (transform.position.x >= 9)
        { movementDir = leftDir; }
    }

    private IEnumerator DiagonalMovements()
    {
        while (true)
        {
            var requiredSpeed = Vector2.Distance(transform.position, movementDir) / 0.2f;

            if (isAtOrigin)
            {
                isAtOrigin = false;
                RandomDiagonalMove();
            }
            if (transform.position.x == originalPos.x && !isAtOrigin)
            {
                isAtOrigin = true;
            }
            else if (transform.position.x <= -9f || transform.position.x >= 9f)
            {
                RandomDiagonalMove();
            }

            transform.position = Vector2.MoveTowards(transform.position, movementDir, requiredSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void RandomDiagonalMove()
    {
        var leftDown = new Vector2(-10f, originalPos.y - 2f);
        var leftUp = new Vector2(-10f, originalPos.y + 2f);

        var rightDown = new Vector2(10f, originalPos.y - 2f);
        var rightUp = new Vector2(10f, originalPos.y + 2f);

        Vector2[] randDir = { leftDown, leftUp, rightDown, rightUp };
        movementDir = randDir[Random.Range(0, randDir.Length)];
    }
}
