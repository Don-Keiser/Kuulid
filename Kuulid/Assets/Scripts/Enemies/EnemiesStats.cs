using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesStats : MonoBehaviour
{
    public static EnemiesStats instance;

    public int eDamage;

    [SerializeField] float eCurrentHealth;
    [SerializeField] float eMaxHealth;

    public float eBulletSpeed;

    [SerializeField] bool isBoss = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        eCurrentHealth = eMaxHealth;
    }

    public float eRemainingHealthPercentage
    {
        get
        {
            return eCurrentHealth / eMaxHealth;
        }
    }

    public UnityEvent OnHealthChange;

    public void TakeDamage(float damageAmount)
    {
        if (eCurrentHealth == 0)
        {
            gameObject.SetActive(false);
            return;
        }

        eCurrentHealth -= damageAmount;

        if (isBoss)
        {
            OnHealthChange.Invoke();
        }

        if (eCurrentHealth < 0)
        {
            eCurrentHealth = 0;
        }
    }
}
