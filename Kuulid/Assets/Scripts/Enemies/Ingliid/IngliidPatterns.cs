using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngliidPatterns : MonoBehaviour
{
    [Header("GameObjects")]
    [SerializeField] GameObject bulletPrefab;
    [Header("Transforms")]
    [SerializeField] Transform[] bulletPositionPattern1;
    [Header("Floats")]
    [SerializeField] float movementSpeed;

    private Vector2 movementDir;

    private void Start()
    {
        StartCoroutine("Shoot");
        RandomStartingDir();
    }

    private void Update()
    {
        HorizontalMovements();
    }

    private void RandomStartingDir()
    {
        Vector2[] randDir = { Vector2.left, Vector2.right };
        movementDir = randDir[Random.Range(0, randDir.Length)];
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.4f);

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

        if (transform.position.x <= -8)
        { movementDir = rightDir; }
        else if (transform.position.x >= 8)
        { movementDir = leftDir; }
    }
}
