using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 5;
    public int currentHealth;
    public int killCount = 0;

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
                killCount++;
                Destroy(gameObject);
            }
        }
        else
        {
            currentHealth -= damage;
        }
    }
}
