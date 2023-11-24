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
            Destroy(gameObject);
            return;
        }

        eCurrentHealth -= damageAmount;

        OnHealthChange.Invoke();

        if (eCurrentHealth < 0)
        {
            eCurrentHealth = 0;
        }
    }
}
