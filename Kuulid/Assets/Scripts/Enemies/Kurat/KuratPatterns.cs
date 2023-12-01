using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KuratPatterns : MonoBehaviour
{
    [Header("Transforms")]
    [SerializeField] Transform bPP11;
    [SerializeField] Transform bPP12;
    [SerializeField] Transform BPP2G;
    [SerializeField] Transform[] bulletPositionPattern2;
    [Header("Floats")]
    [SerializeField] float changeCooldown;
    [SerializeField] float diagonalSetCount;

    //private floats
    private float turboSpiralTimer = 0;
    private float movementSpeed;
    [SerializeField] float diagonalCount;

    //private bools
    private bool isAtOrigin = true;
    private bool isTurboSpiral = false;
    private bool isDiagonalMoving = false;
    private bool rotateShoot = false;

    //private Vectors
    private Vector2 movementDir;
    private Vector3 originalPos;

    private void Start()
    {
        diagonalCount = diagonalSetCount;
        originalPos = transform.position;
        StartCoroutine(DiagonalMovements());
        StartCoroutine(TriangleShoot());
    }

    private void Update()
    {
        if (isTurboSpiral)
        {
            turboSpiralTimer += Time.deltaTime;
        }
        if (turboSpiralTimer > 5)
        {
            isTurboSpiral = false;
        }
        if (isDiagonalMoving)
        {
            diagonalCount -= Time.deltaTime;
        }
    }

    private IEnumerator GoToSpiralPos()
    {
        while (diagonalCount <= 0 && !isTurboSpiral)
        {
            var aproxVector = new Vector2(0, 1);
            movementSpeed = Vector2.Distance(transform.position, movementDir) / 1f;

            transform.position = Vector2.MoveTowards(transform.position, aproxVector, movementSpeed * Time.deltaTime);

            if (Mathf.Approximately(transform.position.x, 0) && Mathf.Approximately(transform.position.y, 1))
            {
                isTurboSpiral = true;
                StartCoroutine(TurboSpiral());
            }
            yield return null;
        }
    }

    private IEnumerator TurboSpiral()
    {
        while (isTurboSpiral)
        {
            yield return new WaitForSeconds(0.02f);

            Transform[] bPPs = { bPP11, bPP12 };

            int randRValue = Random.Range(5, 15);

            foreach (Transform pos in bPPs)
            {
                GameObject bullet = BossObjectPool.instance.GetPooledObject();
                if (bullet != null)
                {
                    bullet.transform.position = pos.position;
                    bullet.transform.localRotation = pos.rotation;
                    bullet.SetActive(true);

                    bPP11.rotation = bPP11.rotation * Quaternion.Euler(0, 0, randRValue);
                    bPP12.rotation = bPP12.rotation * Quaternion.Euler(0, 0, -randRValue);
                }
            }
        }
        turboSpiralTimer = 0f;
        diagonalCount = diagonalSetCount;
        StartCoroutine(DiagonalMovements());
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(TriangleShoot());
    }

    private IEnumerator TriangleShoot()
    {
        while (isDiagonalMoving)
        {
            yield return new WaitForSeconds(0.15f);

            if (!rotateShoot)
            {
                BPP2G.rotation = BPP2G.rotation * Quaternion.Euler(0, 0, 5);

                if (BPP2G.rotation == Quaternion.Euler(0, 0, 20))
                {
                    rotateShoot = true;
                }
            }
            else if (rotateShoot)
            {
                BPP2G.rotation = BPP2G.rotation * Quaternion.Euler(0, 0, -5);

                if (BPP2G.rotation == Quaternion.Euler(0, 0, -20))
                {
                    rotateShoot = false;
                }
            }

            foreach (Transform pos in bulletPositionPattern2)
            {
                GameObject bullet = BossObjectPool.instance.GetPooledObject();

                if (bullet != null)
                {
                    bullet.transform.position = pos.position;
                    bullet.transform.localRotation = pos.rotation;
                    bullet.SetActive(true);
                }
            }
            yield return null;
        }
    }

    private IEnumerator DiagonalMovements()
    {
        while (diagonalCount > 0)
        {
            isDiagonalMoving = true;
            movementSpeed = Vector2.Distance(transform.position, movementDir) / 0.5f;

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

            transform.position = Vector2.MoveTowards(transform.position, movementDir, movementSpeed * Time.deltaTime);

            yield return null;
        }
        isDiagonalMoving = false;
        StartCoroutine(GoToSpiralPos());
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
