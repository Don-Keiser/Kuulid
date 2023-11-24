using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyObjectPool : MonoBehaviour
{
    public static EnemyObjectPool instance;

    [SerializeField] List<GameObject> pooledObjects = new List<GameObject>();
    [SerializeField] int amountToPool;
    [SerializeField] GameObject EnemyBulletPrefab;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(EnemyBulletPrefab);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0;i < pooledObjects.Count;i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
