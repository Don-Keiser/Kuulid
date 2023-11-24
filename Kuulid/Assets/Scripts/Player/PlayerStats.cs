using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public int pDamage;

    [SerializeField] float pCurrentHealth;
    [SerializeField] float pMaxHealth;

    public float pBulletSpeed;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        pCurrentHealth = pMaxHealth;
    }

    public float pRemainingHealthPercentage
    {
        get
        {
            return pCurrentHealth / pMaxHealth;
        }
    }

    public UnityEvent OnHealthChange;

    public void TakeDamage(float damageAmount)
    {
        if (pCurrentHealth == 0)
        {
            return;
        }

        pCurrentHealth -= damageAmount;

        OnHealthChange.Invoke();

        if (pCurrentHealth < 0)
        {
            pCurrentHealth = 0;
        }
    }
}
