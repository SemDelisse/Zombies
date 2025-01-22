using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    public void TakeDamage(int damage)
    {
        if (currentHealth <= 1)
        {
            if (gameObject.CompareTag("Player"))
            {
                
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                //ScoreSystem.Instance.AddKill(1);
                Destroy(gameObject);
            }
        }
        else
        {
            currentHealth -= damage;
        }
    }
}
